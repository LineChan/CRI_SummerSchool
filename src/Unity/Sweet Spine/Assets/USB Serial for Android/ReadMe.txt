#### USB Serial for Android
by NGC6543(ngc6543@me.com)
Based on the library usb-serial-for-android.
(https://github.com/mik3y/usb-serial-for-android)


*** Supported drivers

 1. Drivers that support CDC-ACM(Communication Device Class - Abstract Control Model)
 2. CH34x serial adapters
 3. CP21xx USB to UART drivers
 4. FTDI serial drivers
 5. Prolific USB to UART drivers


*** How to use?

- USBSerialForAndroid.cs is a Singleton. It has DonDestroyOnLoad(). 
 Add the prefab into a scene, and access it through USBSerialForAndroid.instance.

- BaudRate is fixed to 115200. If needed, change the source code(USBSerialForAndroid.cs).

- Call in this order :
	1. RefreshUSBDevice() <- Not mandatory anymore!!
	2. (Optional) GetDeviceList() //can throw null reference exception if called right after RefreshUSBDevice()
	3. ConnectToDevice0() or ConnectToDevice(int index)
	4. DisconnectDevice()

- Serial Data example
	Data : 124.4+433.12+-432.1/124.1+-0.32+-499/131.4+
	Above data example has two complete data packets and one partial data packet.
	- separator : + ; split data element inside a data packet
	- tail : / ; split data packet from serial data stream
	So the final data packets are : (124.4, 433.12, -432.1), (124.1, -0.32, -499)

- maxBufferCharCount : Sets the maximum count for the buffer ‘Queue<char> bufferQueue’ 
 to prevent buffer overflow exception.

- EvaluateReceivedMessage() evaluates the received message(data packet). 
 You can customize it by either changing the source code or inheriting and overriding it.


*** Change Logs
Ver 1.1.3
- Fixed issues with the target devices other than Android causes build errors.

Ver 1.1.2
- Fixed onSerialDataAcceptedString to onStringAccepted.
- Fixed onSerialDataAcceptedFloatArray to onFloatArrayAccepted.
- Added IntArrayEvent onIntArrayAccepted event. You can now use int array data.

Ver 1.1.1
- Added a warning if bufferQueue.Count exceeds maxBufferCharCount. 
 This may indicate that you have set the wrong 'Tail' char, or too little maxBufferCharCount.
- Fixed System.InvalidCastException to System.FormatException on EvaluateReceivedMessage().
 This may indicate that you have set the wrong 'Separator' char. Make suer that your serial device
 sends data with the same 'Separator' char.
- Added string ErrorMsg property. If an error occurs, the detail is given through this property.

Ver 1.1.0
- Added OnConnect / OnDisconnect events. 
- Now, RefreshUSBDevice() is automatically done periodically inside the library 
 to check if the connected device was lost. So, the user doesn't have to refresh it manually.

Ver 1.0.0

