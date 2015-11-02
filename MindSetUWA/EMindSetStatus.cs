namespace MindSetUWA
{
    //DODĚLAT
    public enum EMindSetStatus
    {
        Connecting, //Connection in progress

        ConnectedBT, //Connected to a bluetooth headset

        ConnectedTG, //Connected to a RF headset using ThinkGear Connector

        BTConnectionFail, //Failed Bluetooth connection (Is HeadSet paired to device ?)

        TGConnectionFail, //Failed ThinkGear connection (Is ThinkGear installed ?)

        ParseFail, //Packet parsing error

        LowEEGSignal // EEG Signal level is NOT 0
    }
}
