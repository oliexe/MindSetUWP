using MindSetUWA.Common;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace MindSetUWA
{
    public partial class MindSetConnection : IMindwave
    {
        private StreamSocket socket;
        private DataReader reader;

        public MindsetDataStruct RealtimeData = new MindsetDataStruct();
        private EMindSetStatus Status = new EMindSetStatus();

        private const int PacketLenght = 36; //MindSet packet is 36 bytes long.

        public EMindSetStatus ConnectionStatus()
        {
            return Status;
        }

        public String ConnectionStatusString()
        {
            return Status.ToString();
        }

        /// <summary>
        /// Opens a bluetooth connnection to a MindWave Mobile headset.
        /// Please specify a Bluetooth name (Usually "MindWave Mobile") in the first parameter.
        /// </summary>
        public async void ConnectBluetooth(String BTname)
        {
            try
            {
                Status = EMindSetStatus.Connecting;
                var BluetoothZarizeni = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));
                var MindWaveHeadset = BluetoothZarizeni.SingleOrDefault(d => d.Name == BTname);
                var serviceRfcomm = await RfcommDeviceService.FromIdAsync(MindWaveHeadset.Id);

                socket = new StreamSocket();
                await socket.ConnectAsync(serviceRfcomm.ConnectionHostName, serviceRfcomm.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

                reader = new DataReader(socket.InputStream);
                ParseHeadsetPackets();
            }
            catch
            {
                Status = EMindSetStatus.BTConnectionFail;
            }
        }

        /// <summary>
        /// Closes a already established bluetooth connnection to a MindWave Mobile headset.
        /// </summary>
        public void Disconnect()
        {
            socket.Dispose();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private async void ParseHeadsetPackets()
        {
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
                        // Check if enough data exists to finalize this useful data packet, if not, get another
                        if (indexOfUsefulDataHeader.Value + PacketLenght > resultArray.Length)
                        {
                            var nextResultsArray = await NextBuffer();
                            resultArray = resultArray.Concat(nextResultsArray).ToArray();
                        }

                        // Packet is all right
                        var PctData = resultArray.Skip(indexOfUsefulDataHeader.Value).Take(PacketLenght + 4).ToArray();
                        Status = EMindSetStatus.ConnectedBT;

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
                    }
                }
            }
            catch
            {
                Status = EMindSetStatus.ParseFail;
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
