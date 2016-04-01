
![Logo](https://cloud.githubusercontent.com/assets/441290/14204753/a2e3fb4c-f806-11e5-97a0-d78f83d5b533.jpg)

Visit GitHub Page : http://olirehacek.github.io/MindSetUWP/

MindWaveUWP makes your universal Windows apps smarter and helps them to read user brainwave data using Bluetooth EEG sensors. The library is specifically designed to be very easy, fast and deployable with minimal alterations of your already working code. With only 2 extra lines of code your application can be ready to recieve and process users brainwave data. The library does not work only with real-time EEG readings, but also provides built-in data recording for long-term data analysis.

Library is designed for use with Bluetooth equipped MindWave Mobile EEG headset. This device is the culmination of decades of EEG biosensor technology research—all in one easy-to-control, wearable package. As the most affordable brainwave-reading EEG headset.

## Changelog

[1-30-2016] Updated to Windows 10 SDK version 10.0.10586 (Latest)

[1-12-2016] The library now includes recording of the EEG data.

[18-20-2015] Updated to Windows 10 SDK version 10.0.10240

## Implementing MindSetUWP into your application
```
private MindSetConnection MyHeadset = new MindSetConnection()
```

## Establishing Bluetooth connection
Now we can use a method "ConnectBluetooth" followed by suppling the bluetooth name of the (In most cases the name is "MindWave Mobile") to initiate the packet connection to the EEG headset - that is all, easy as that - now we are recieving the formatted EEG readings from the headset.
```
private void EstConnBtn_Click(object sender, RoutedEventArgs e)
{
MyHeadset.ConnectBluetooth("MindWave Mobile");
}
```

## EEG Data
MindSet headset transmits ThinkGear Data Values, encoded within ThinkGearPackets, as a serial stream of bytes over Bluetooth via a standard Bluetooth Serial Port Profile (SPP). MindSetUWP library process all of this packets and manages bluetooth connection with headset for you, so you are no longer required to do any extra work.

All of the variables in the table represent particular EEG data recieved from the MindWave headset. They are publicly acessible variables in the instance you created and connected to a headset using "RealtimeData" class :
```
private void ShowDataBtn_Click(object sender, RoutedEventArgs e)
{
Debug.Write(MyHeadset.RealtimeData.AlphaHigh); 
Debug.Write(MyHeadset.RealtimeData.AlphaLow); //WritesAlphaLow data into debug console.
}
```

## Handling events
Library offers multiple events that you can use. We need to work with StatusUpdateHandler that is included in library to handle these events and project them into your application.

```
MyHeadset.Connected += newMindSetConnection.StatusUpdateHandler(OnConnected);
MyHeadset.NoHeadset += newMindSetConnection.StatusUpdateHandler(NoHeadsetFound);
```

# Recording EEG activity

### Recording
Library also includes internal recording of the packets. Using this we can work with EEG data from long time period and analyze them further in your application. The recording is also configurable.

#### Recording Density
We can set the RecordDensity - a integer number that identifies how many recieved packets to record (For example if the RecordingDensity is set to "1" every recieved packet is recorded, if the RecordingDensity is set to "10" an every tenth packet is recorded).

#### Recording 
To obtain only reliable data in your recording, we can use RecordingFiltering. This filter sets the recording to record only data where Quality of recieved EEG data is reliable and eSense data is recieved. If the recording filtering is turned off, every recieved data is recorded, even if the headset is misplaced on user head, or packet data are not reliable. So turning on filtering is recommended.

To start recording we need to use only one line of code, please note the arguments explained in the commented line:
```
//MyHeadset.StartRecording(RecordingDensity, RecordingFiltering);
MyHeadset.StartRecording(1, true);
```

Same as when we want to pause the recording:
```
MyHeadset.StopRecording;
```

Please note that this command only holds the recording and if we start the recording again, the new data will be added to the old one. If we want to clear already recorded data we can use:
```
MyHeadset.ClearRecordingData();
```

### Working with recorded data
Working with recorded data is same as with the realtime data. Only exception is that the recorded data are storred in the array. To obtain the recorded data saved in the memory use the function called "RecordingToArray". A great example is this FOR loop, that prints every recieved packet in string format into debug window.
```
MindsetDataStruct[] MyRecordedData = MyHeadset.RecordingToArray();
for (int i = 0; i < MyRecordedData.Length; i++)
{
Debug.WriteLine(MyRecordedData[i].AllToString);
}
```
