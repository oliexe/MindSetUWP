using System;
using Windows.Networking;

namespace MindSetUWA
{
    public interface IMindwave : IDisposable
    {
        /// <summary>
        /// Opens a bluetooth connnection to a MindWave Mobile headset.
        /// Please specify a Bluetooth name (Usually "MindWave Mobile") in the first parameter.
        /// </summary>
        void ConnectBluetooth(String BTname);

        /// <summary>
        /// LEGACY MODE - Opens a packet connnection to a legacy MindWave headset that uses 2.4Ghz RF dongle.
        /// However, this is NOT compatible in Windows Phone platform and require a ThinkGear Connector installed on a target machine.
        /// Specify a hostname (eg. localhost) and port
        /// </summary>
        void ThinkGearConnect(HostName hostname, string port);

        /// <summary>
        /// Closes a already established connection to a headset.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Returns a realtime status of the connection to a headset in MindSetUWA.EMindSetStatus format.
        /// </summary>
        EMindSetStatus ConnectionStatus();

    }
}
