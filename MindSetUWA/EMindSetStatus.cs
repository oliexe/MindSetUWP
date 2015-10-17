using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSetUWA
{
    public enum EMindSetStatus
    {
        Connecting,

        Connected,

        BTConnectionFail,

        ParseFail,

        LowEEGSignal
    }
}
