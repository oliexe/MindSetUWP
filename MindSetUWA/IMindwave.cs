using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace MindSetUWA
{
    public interface IMindwave : IDisposable
    {
        /// <summary>
        /// Opens a bluetooth connnection to a MindWave Mobile headset.
        /// Please specify a Bluetooth name (Usually "MindWave Mobile") in the first parameter.
        /// </summary>
        void Connect(String BTname);

        /// <summary>
        /// Closes a already established bluetooth connnection to a MindWave Mobile headset.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Returns a realtime status of the connection to a headset in MindSetUWA.EMindSetStatus format.
        /// </summary>
        EMindSetStatus ConnectionStatus();

        void ParseHeadsetPackets();

        /// <summary>
        /// Return a realtime status of the connection to a headset in string format.
        /// </summary>
        string ConnectionStatusString();

    }
}
