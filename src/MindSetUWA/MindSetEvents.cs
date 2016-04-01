using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSetUWP
{
    public partial class MindSetConnection
    {

        public delegate void StatusUpdateHandler(object sender, ProgressEventArgs e);

        public event StatusUpdateHandler Connected;
        public event StatusUpdateHandler Connecting;
        public event StatusUpdateHandler PacketRecieved;
        public event StatusUpdateHandler Disconnected;
        public event StatusUpdateHandler NoHeadset;
        public event StatusUpdateHandler QualityChange;
        public event StatusUpdateHandler ParseFail;
        public event StatusUpdateHandler Recording;
        public event StatusUpdateHandler RecordingStopped;


        private void RaiseConnecting()
        {
            Status = EMindSetStatus.Connecting;
            if (Connecting == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Connecting");
            Connecting(this, args);
        }

        private void RaisePacketRecieved()
        {
            if (PacketRecieved == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Packet recieved");
            PacketRecieved(this, args);
        }

        private void RaiseConnected()
        {
            Status = EMindSetStatus.ConnectedBT;
            if (Connected == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Connected");
            Connected(this, args);
        }

        private void RaiseNoHeadset()
        {
            Status = EMindSetStatus.BTConnectionFail;
            if (NoHeadset == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Headset not found");
            NoHeadset(this, args);
        }

        private void RaiseDisconnected()
        {
            if (Disconnected == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Disconnected");
            Disconnected(this, args);
        }

        private void RaiseParseFail()
        {
            Status = EMindSetStatus.ParseFail;
            if (ParseFail == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Packet parsing failed");
            ParseFail(this, args);
        }

        private void RaiseRecording()
        {
            if (Recording == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Recording");
            Recording(this, args);
        }

        private void RaiseStopRecording()
        {
            if (RecordingStopped == null) return;
            ProgressEventArgs args = new ProgressEventArgs("Recording stopped");
            RecordingStopped(this, args);
        }

        private void RaiseQualityChange(string quality)
        {
            if (QualityChange == null) return;
            ProgressEventArgs args = new ProgressEventArgs(quality);
            QualityChange(this, args);
        }
    }

    public class ProgressEventArgs : EventArgs
    {
        public string Status { get; private set; }

        public ProgressEventArgs(string status)
        {
            Status = status;
        }
    }
}
