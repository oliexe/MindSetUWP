using Windows.Storage.Streams;

namespace MindSetUWA.Common
{
    public static class BufferBytes
    {
        public static byte[] Get(IBuffer buffer)
        {
            using (var dr = DataReader.FromBuffer(buffer))
            {
                byte[] bytes = new byte[dr.UnconsumedBufferLength];
                dr.ReadBytes(bytes);
                return bytes;
            }
        }
    }

    public static class HeaderIndex
    {
        // viz MindWave packet dokumentace
        private const int PARSER_SYNC = 0xAA;
        private const int UsefulDataPacketLength = 32;

        public static int? Get(byte[] resultArray)
        {
            for (int i = 0; i < resultArray.Length - 2; i++)
            {
                if (resultArray[i] == PARSER_SYNC
                    && resultArray[i + 1] == PARSER_SYNC
                    && resultArray[i + 2] == UsefulDataPacketLength)
                {
                    return i;
                }
            }
            return null;
        }
    }

    public static class PacketValue
    {
        public static int Get(byte[] usefulDataPacket, int beginInclusive, int endInclusive)
        {
            // viz: http://www.java2s.com/Code/CSharp/Data-Types/ReadInt24frombytearray.htm

            return (usefulDataPacket[beginInclusive] << 16)
                + (usefulDataPacket[beginInclusive + 1] << 8)
                + (usefulDataPacket[beginInclusive + 2]);
        }
    }
}
