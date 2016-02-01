using System;
using Windows.Networking;

namespace MindSetUWA
{
    public interface IMindwave : IDisposable
    {
        void ConnectBluetooth(String BTname);

        void Disconnect();

        EMindSetStatus ConnectionStatus();
    }
}
