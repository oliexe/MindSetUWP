using MindSetUWA.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using Windows.Storage;
using Windows.Storage.Streams;

namespace MindSetUWA
{
    public partial class MindSetConnection : IMindwave
    {
        //Socket-related variables
        private StreamSocket socket;
        private DataReader reader;

        //Recording variables
        //If enabled the recording is in progress.
        private bool RecordingEnabled = false;

        //Recording Filtering
        //If enabled we are recording only reliable EEG data - that means the signal quality is 0 (Highest level)
        //and the esense data (Attention,Meditation) are recieved.
        private bool RecordingFiltering = false;

        //Recording fidelity settings
        //Maximum fidelity is 1 - recording every packet. (Generally every second)
        //eg. if fidelity is 15 - we are recording every 15th packet. (Generally every 15 seconds)
        private int RecordingFidelity = 1;

        //Recorded data list
        //A list of MindsetDataStruct items that represents idividual recorded packets.
        public List<MindsetDataStruct> RecordedData = new List<MindsetDataStruct>();

        //Realtime EEG data variables
        public MindsetDataStruct RealtimeData = new MindsetDataStruct();

        //Connection status
        public EMindSetStatus Status;
        


        private const int PacketLenght = 36;  //MindSet packet is 36 bytes long. Check the official Documentation for more details.
        int PacketNum = 0;

        //returns status (TODO)
        public EMindSetStatus ConnectionStatus()
        {
            return Status;
        }

        /// <summary>
        /// Opens a bluetooth connnection to a MindWave Mobile headset.
        /// Please specify a Bluetooth name (Usually "MindWave Mobile") in the first parameter.
        /// </summary>
        public async void ConnectBluetooth(String BTname)
        {
            RaiseConnecting();
            try
            {
                var BluetoothZarizeni = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
                var MindWaveHeadset = BluetoothZarizeni.SingleOrDefault(d => d.Name == BTname);
                var serviceRfcomm = await RfcommDeviceService.FromIdAsync(MindWaveHeadset.Id);

                socket = new StreamSocket();
                await socket.ConnectAsync(serviceRfcomm.ConnectionHostName, serviceRfcomm.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

                reader = new DataReader(socket.InputStream);
                RaiseConnected();

                ParseHeadsetPackets();
            }
            catch
            {
                RaiseNoHeadset();
            }
        }

        /// <summary>
        /// Closes a already established bluetooth connnection to a MindWave Mobile headset.
        /// </summary>
        public void Disconnect()
        {
            socket.Dispose();
            this.Dispose();
            RaiseDisconnected();
        }

        public void Dispose()
        {
           
        }

        private async void ParseHeadsetPackets()
        {
            int QualityBuffer = 0;
            try
            {
                while (true)
                {
                    var resultArray = await NextBuffer();

                    int? indexOfUsefulDataHeader = HeaderIndex.Get(resultArray);

                    if (indexOfUsefulDataHeader.HasValue == false)
                    {
                        // ignore data and just dump it
                    }
                    else
                    {
                        // PACKET PARSING BLOCK
                        // Packet Check
                        if (indexOfUsefulDataHeader.Value + PacketLenght > resultArray.Length)
                        {
                            var nextResultsArray = await NextBuffer();
                            resultArray = resultArray.Concat(nextResultsArray).ToArray();
                        }

                        // Packet OK
                        var PctData = resultArray.Skip(indexOfUsefulDataHeader.Value).Take(PacketLenght + 4).ToArray();
                        RaisePacketRecieved();

                        // Check out:
                        // http://wearcam.org/ece516/mindset_communications_protocol.pdf
                        
                        RealtimeData = new MindsetDataStruct(PctData[4], //Signal Quality
                            PacketValue.Get(PctData, 7, 9), //Delta
                            PacketValue.Get(PctData, 10, 12), //Theta
                            PacketValue.Get(PctData, 13, 15), //Low Alpha
                            PacketValue.Get(PctData, 16, 18), //High Alpha
                            PacketValue.Get(PctData, 19, 21), //Low Beta
                            PacketValue.Get(PctData, 22, 24), //High Beta
                            PacketValue.Get(PctData, 25, 27), //Low Gamma
                            PacketValue.Get(PctData, 28, 30), //Mid Gamma
                            PctData[32], //Attention
                            PctData[34], //Meditation
                            DateTime.Now //Timestamp of recieved data
                            );

                        //Raising the quality change event (If necessary).
                        if(RealtimeData.Quality != QualityBuffer)
                        {
                            RaiseQualityChange(RealtimeData.Quality.ToString());
                        }

                        QualityBuffer = RealtimeData.Quality;

                        //RECORDING BLOCK 
                        if (RecordingEnabled)
                        {
                        //Count packets while recording
                        PacketNum++;
                        //Number of "skipped" packets is matching or is grater than recording fidelity, so we want to record this packet.
                        if (PacketNum >= RecordingFidelity)
                        {
                           //The filtering is enabled so record only good quality packets (Signal quality = 0 and eSense data > 0).
                            if (RecordingFiltering && RealtimeData.Quality == 0 && RealtimeData.Meditation > 0 && RealtimeData.Attention > 0)
                                {
                                    //Adding the packet to the recorded packet list.
                                    RecordedData.Add(RealtimeData);
                                    //The packet is recorded so we reset the counter.
                                    PacketNum = 0;
                                }
                            //The filtering is not enabled so we record every packet, even the bad quality ones.
                            if (!RecordingFiltering)
                            {
                                    RecordedData.Add(RealtimeData);
                                    PacketNum = 0;
                            }
                        }
                        }
                    }
                    }   
            }
            catch
            {
                RaiseParseFail();
            }
        }

        /// <summary>
        /// Starts recording of the stream of packets coming from headset.
        /// </summary>
        public void StartRecording(int RecFidelity, bool filtering)
        {
            RecordingEnabled = true;
            RecordingFidelity = RecFidelity;
            RecordingFiltering = filtering;
            RaiseRecording();
        }

        /// <summary>
        /// Pauses a already established recording of packets.
        /// </summary>
        public void StopRecording()
        {
            if(RecordingEnabled)
            {
            RecordingEnabled = false;
            RaiseStopRecording();
            }
        }

        /// <summary>
        /// Exports a already recorded data into Array string.
        /// </summary>
        public MindsetDataStruct[] RecordingToArray()
        {
            RecordingEnabled = false;
            return RecordedData.ToArray();
        }

        /// <summary>
        /// Clears a recorded packets.
        /// </summary>
        public void ClearRecordingData()
        {
            if (!RecordingEnabled)
            {
            RecordingEnabled = false;
            RecordedData.Clear();
            }
        }

        /// <summary>
        /// Gets next packet data.
        /// </summary>
        private async Task<byte[]> NextBuffer(uint length = 512)
        {
            var buffer = await socket.InputStream.ReadAsync(new Windows.Storage.Streams.Buffer(length), length, InputStreamOptions.None);

            var resultArray = BufferBytes.Get(buffer);

            return resultArray;
        }
    }
}
