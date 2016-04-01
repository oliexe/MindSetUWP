using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSetUWP
{
    public class ConnectionException : Exception
    {
        public ConnectionException()
        {
            //put your custom code here
        }
    }

    public class ParseException : Exception
    {
        public ParseException()
        {
            //put your custom code here
        }
    }

    public class RecordingNotStopped: Exception
    {
        public RecordingNotStopped()
        {
            //put your custom code here
        }
    }

    public class RecordingNotStarted : Exception
    {
        public RecordingNotStarted()
        {
            //put your custom code here
        }
    }

    public class NotConnected : Exception
    {
        public NotConnected()
        {
            //put your custom code here
        }
    }

    public class AlreadyConnected : Exception
    {
        public AlreadyConnected()
        {
            //put your custom code here
        }
    }
}