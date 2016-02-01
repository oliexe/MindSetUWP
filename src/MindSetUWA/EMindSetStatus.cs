namespace MindSetUWA
{
    //DODĚLAT
    public enum EMindSetStatus
    {
        Connecting, //Connection in progress

        ConnectedBT, //Connected to a bluetooth headset

        BTConnectionFail, //Failed Bluetooth connection (Is HeadSet paired to device ?)

        ParseFail //Packet parsing error
    }
}
