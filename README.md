![alt tag](http://s10.postimg.org/fnqxqgx6h/win10_logo.jpg)
A NeuroSky MindWave and MindWave Mobile EEG headset library for Windows 10 universal applications ecosystem. A part of my bachelor thesisat VŠB - Technical University of Ostrava. Study year 2015-2016. Library isdesigned primary for use with MindWave Mobile bluetooth headset paired with Windows 10 device. You can easilly recieve EEG readings to any UWAapplication and use them

### Basic Usage
Library can be used for any .NET Windows 10 Universal application running on every Windows 10 device that have a bluetooth adapter installed with appropriate system drivers. The EEG headset must be paired in the system Bluettooth menu prior to the use of the library itself.

## Implementing MindSetUWP into your application
```
private MindSetConnection MyHeadset = new MindSetConnection()
```

## Establishing Bluetooth connection
Now we can use a method "ConnectBluetooth" followed by suppling the bluetooth name of the (In most cases the name is "MindWave Mobile") toinitiate the packet connection to the EEG headset - that is all, easy as that - now we are recieving the formatted EEG readings from the headset.
```
private void EstConnBtn_Click(object sender, RoutedEventArgs e)
{
MyHeadset.ConnectBluetooth("MindWave Mobile");
}
```

## EEG Data
MindSet headset transmits ThinkGear Data Values, encoded within ThinkGearPackets, as a serial stream of bytes over Bluetooth via a standard BluetoothSerial Port Profile (SPP). MindSetUWP library process all of this packets 2 and manages bluetooth connection with headset for you, so you are no longer required to do any extra work. Before we start working with the data , lets talk about all measurements that headset send to your applicaion and this library supports.

All of the variables in the table represent particular EEG data recievedfrom the MindWave headset, they are publicly acessible variables in the instance you created and connected to a headset using "RealtimeData" class.
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
Library also includes internal recording of the packets. Using this we can work with EEG data from longer time perion and analyse them further in your application. The recording is also deeply configurable.

#### Recording Density
We can set the RecordDensity - a integer number that identifies how many recieved packets to record (For example if the RecordingDensity is set to "1" every recieved packet is recorded, if the RecordingDensity is set to "10" an every tenth packet is recorded).

#### Recording 
To obtain only reliable data to your recording, we can use RecordingFiltering. This filter sets the recording to record only data where Quality of recieved EEG data is reliable and eSense data is recieved. Please note that if the recording filtering is turned off, every recieved data is recorded, even if the headset is misplaced on user head, or packet data are not reliable.

To start recording we need to use only one line of code, please note thearguments explained in the commented line:
```
//MyHeadset.StartRecording(RecordingDensity, RecordingFiltering);
MyHeadset.StartRecording(1, true);
```

Same as when we want to pause the recording:
```
MyHeadset.StopRecording;
```

Please note that this command only pauses the recording and if we start the recording again, the new data will be added to the old ones. If we want to clear already recorded data on the other hand, use:
```
MyHeadset.ClearRecordingData();
```

### Working with recorded data
We can work with recorded data same as with the realtime data. Only ex-ception is that the recorded data are storred in the array. To obtain the recorded data saved in the memory use the function called "RecordingToArray". A great example is this FOR loop, that prints every recieved packet instring format into debug window.
```
MindsetDataStruct[] MyRecordedData = MyHeadset.RecordingToArray();
for (int i = 0; i < MyRecordedData.Length; i++)
{
Debug.WriteLine(MyRecordedData[i].AllToString);
}
```
