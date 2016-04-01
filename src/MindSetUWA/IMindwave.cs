using System;
using Windows.Networking;

namespace MindSetUWP
{
    public interface IMindwave : IDisposable
    {
        void ConnectBluetooth(String BTname);

        void Disconnect();

        EMindSetStatus ConnectionStatus();
    }
}
