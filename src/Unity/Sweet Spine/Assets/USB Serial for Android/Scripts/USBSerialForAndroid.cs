using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class USBSerialForAndroid : MonoBehaviour {

	public static USBSerialForAndroid instance;

	[System.Serializable]
	public class StringEvent : UnityEvent<string>{}
	[System.Serializable]
	public class FloatArrayEvent : UnityEvent<float[]>{}
	[System.Serializable]
	public class IntArrayEvent : UnityEvent<int[]>{}

	[Header("Serial connection events")]
	public UnityEvent onConnect;
	public UnityEvent onDisconnect;

	[Header("Serial data events")]
	public bool invokeString;
	public StringEvent onStringAccepted;	// Raw string data are received. send raw message.
	public bool invokeIntArray;
	public IntArrayEvent onIntArrayAccepted;	// Data of int array are received without an error. Send int[] array
	public bool invokeFloatArray;
	public FloatArrayEvent onFloatArrayAccepted; 	// Data of float array are received without an error. Send float[] array
	public UnityEvent onSerialDataDeclined;		// data received with error(s)

	public enum DATABITS{ FIVE=5, SIX=6, SEVEN=7, EIGHT=8 };
	public enum PARITY{ NONE, ODD, EVEN, MARK, SPACE };
	public enum STOPBITS{ ONE=1, ONEPOINTFIVE=3, TWO=2 };

	[Header("Serial Comm settings")]
	[SerializeField] DATABITS dataBits = DATABITS.EIGHT;
	[SerializeField] STOPBITS stopBits = STOPBITS.ONE;
	[SerializeField] PARITY parity = PARITY.NONE;

	[Header("Serial data settings")]
	//[SerializeField] char head = '';
	[Tooltip("Distinguish between each data elements"),
	 SerializeField] char separator = '+';
	[Tooltip("Distinguish between data packets"),
	 SerializeField] char tail = '/';
	[SerializeField, Range(10, 100)] int maxBufferCharCount = 30;

	[Header("Serial data")]
	[SerializeField] protected string receivedMessage;
	[SerializeField] protected string[] splitMessages;
	[SerializeField] protected int[] intResults;
	[SerializeField] protected float[] floatResults;

	Queue<char> bufferQueue;
	List<char> tempBuffer;

	[Header("Debug")]
	[SerializeField] Text msg;
	[SerializeField] Button btnRefreshDevice;
	[SerializeField] Button btnConnectDevice0;
	[SerializeField] Button btnDisconnectDevice;

	public string ErrorMsg{get; private set;}

	AndroidJavaClass usbSerialForAndroid;
	AndroidJavaObject androidInstance;

	void Awake(){
		if(instance == null){
			DontDestroyOnLoad(gameObject);
			instance = this;	

			bufferQueue = new Queue<char>();
			tempBuffer = new List<char>();

			if(btnRefreshDevice != null)
				btnRefreshDevice.onClick.AddListener(OnRefreshDeviceButtonClicked);
			if(btnConnectDevice0 != null)
				btnConnectDevice0.onClick.AddListener(OnConnectDevice0Clicked);
			if(btnDisconnectDevice != null)
				btnDisconnectDevice.onClick.AddListener(OnDisconnectDeviceClicked);

			ErrorMsg = "";
	#if UNITY_EDITOR

	#elif UNITY_ANDROID
		usbSerialForAndroid = new AndroidJavaClass("com.hoho.android.usbserial.util.USBSerialForAndroid");
		usbSerialForAndroid.CallStatic("Start", gameObject.name, 115200, (int)dataBits, (int)stopBits, (int)parity);
		androidInstance = usbSerialForAndroid.GetStatic<AndroidJavaObject>("instance");
	#endif
		}
		else{
			Debug.LogWarning("USBSerialForAndroid should be a singleton!");
			DestroyImmediate(this);
			return;
		}
	}

	void Start(){

	}

	void OnRefreshDeviceButtonClicked(){
		RefreshUSBDevice();
	}
	void OnConnectDevice0Clicked(){
		ConnectToDevice0();
	}
	void OnDisconnectDeviceClicked(){
		DisconnectDevice();
	}

	public void RefreshUSBDevice(){
	#if UNITY_EDITOR
		Debug.LogWarning("JNI native code : not supported in the Editor.");
#elif UNITY_ANDROID
		androidInstance.Call("RefreshDeviceList");
		StartCoroutine(DelayedGetDeviceList());
		#else
		Debug.LogWarning("JNI native code : Only supported on Android device!");
		return;
#endif

	}
	IEnumerator DelayedGetDeviceList(){
		#if UNITY_EDITOR
		Debug.LogWarning("JNI native code : not supported in the Editor.");
		yield return null;
		#elif UNITY_ANDROID
		yield return new WaitForSeconds(0.1f);
		string[] deviceList = GetDeviceList();
		string s = "#### Device list\n";
		for (int i = 0; i < deviceList.Length; i++)
		{
			s += i + " : " + deviceList[i] + "\n";
		}
		Debug.Log(s);
		#else
		Debug.LogWarning("JNI native code : Only supported on Android device!");
		yield return null;
		#endif
	}
	public string[] GetDeviceList(){
		#if UNITY_EDITOR
		Debug.LogWarning("JNI native code : not supported in the Editor.");
		return new string[]{"NOT SUPPORTED"};
		#elif UNITY_ANDROID
		try{
			return androidInstance.Call<string[]>("GetDeviceList");
		}catch(System.NullReferenceException e){
			return new string[]{"Error"};
		}
		#else
		Debug.LogWarning("JNI native code : Only supported on Android device!");
		return new string[]{"NOT SUPPORTED"};
		#endif
	}

	public bool ConnectToDevice0(){
		return ConnectToDevice(0);
	}

	public bool ConnectToDevice(int index){
		#if UNITY_EDITOR
		Debug.LogWarning("JNI native code : not supported in the Editor.");
		return false;
		#elif UNITY_ANDROID
		if(androidInstance.Call<bool>("ConnectToDevice", index)){
			Debug.Log("Device"+index+" connected!");
			return true;
		}else{
			if(msg!=null)	msg.text = "Failed to connect to the Device"+index+".";
			Debug.Log("Failed to connect to the Device"+index+".");
			return false;
		}
		#else
		Debug.LogWarning("JNI native code : Only supported on Android device!");
		return false;
		#endif
	}

	public void DisconnectDevice(){
		#if UNITY_EDITOR
		Debug.LogWarning("JNI native code : not supported in the Editor.");
		#elif UNITY_ANDROID
		androidInstance.Call("DisconnectDevice");
		#else
		Debug.LogWarning("JNI native code : Only supported on Android device!");
		return;
		#endif
	}

	/// <summary>
	/// Invoked when a serial data has been received.
	/// data protocol example (3-axis acceleration)
	/// ##+##+##/ 
	/// </summary>
	/// <param name="data">Data.</param>
	public void OnSerialDataReceived(string data){
		
		var tempChars = data.ToCharArray();
		for (int i = 0; i < tempChars.Length; i++)
		{
			bufferQueue.Enqueue(tempChars[i]);
		}

		// Check data and see if there is a tail(predefined finish character)
		while(true){
			char c = bufferQueue.Dequeue();
			if(c == tail) break;
			tempBuffer.Add(c);
			if(bufferQueue.Count == 0) return;	// return and wait for next data to come
		}
		// A packet is created. Validate the packet.
		receivedMessage = new string(tempBuffer.ToArray());
		tempBuffer.Clear();
		if(bufferQueue.Count > maxBufferCharCount) {
			bufferQueue.Clear(); // if Queue count exceeds the buffer limit, force clear.
			string warning = "Max buffer char count reached. Increase the value, or, is the Tail char '" + tail.ToString()+"' correct?";
			if(msg!=null) msg.text = warning;
			Debug.LogWarning(warning);
		}

		EvaluateReceivedMessage();
	}


	/// <summary>
	/// Implement this method to evaluate the receivedMessage.
	/// (Optional)If the message is correct, invoke onSerialDataAccepted(String/float[] or both).
	/// (Optional)Otherwise, invoke onSerialDataDeclined.
	/// </summary>
	protected virtual void EvaluateReceivedMessage(){
		if(invokeString){
			onStringAccepted.Invoke(receivedMessage);
		}
		if(invokeIntArray){
			// try parsing
			splitMessages = receivedMessage.Split(new char[]{separator});
			intResults = new int[splitMessages.Length];
			//Debug.Log("Result : " + result.Length + " strings. Converting to float...");

			try{
				for (int i = 0; i < splitMessages.Length; i++)
				{
					intResults[i] = int.Parse(splitMessages[i]);
				}
			}catch(System.FormatException e){
				//Debug.LogError(e);
				string err = "Format Exception occured :\n";
				for (int i = 0; i < splitMessages.Length; i++)
				{
					err += "str"+i + " " + splitMessages[i]+", ";
				}
				err +="\nIs the Separator char '"+separator.ToString()+"' correct?";
				if(msg!=null) msg.text = err;
				ErrorMsg = err;
				Debug.LogError(err);
				onSerialDataDeclined.Invoke();
				return;
			}

			//string success = "Data successfully parsed : ";
			//for (int i = 0; i < result.Length; i++)
			//{
			//	success += "data"+i+" "+data[i]+", ";
			//}
			//Debug.Log(success);
			onIntArrayAccepted.Invoke(intResults);
		}
		if(invokeFloatArray){
			// try parsing
			splitMessages = receivedMessage.Split(new char[]{separator});
			floatResults = new float[splitMessages.Length];
			//Debug.Log("Result : " + result.Length + " strings. Converting to float...");

			try{
				for (int i = 0; i < splitMessages.Length; i++)
				{
					floatResults[i] = float.Parse(splitMessages[i]);
				}
			}catch(System.FormatException e){
				//Debug.LogError(e);
				string err = "Format Exception occured :\n";
				for (int i = 0; i < splitMessages.Length; i++)
				{
					err += "str"+i + " " + splitMessages[i]+", ";
				}
				err +="\nIs the Separator char '"+separator.ToString()+"' correct?";
				if(msg!=null) msg.text = err;
				ErrorMsg = err;
				Debug.LogError(err);
				onSerialDataDeclined.Invoke();
				return;
			}

			//string success = "Data successfully parsed : ";
			//for (int i = 0; i < result.Length; i++)
			//{
			//	success += "data"+i+" "+data[i]+", ";
			//}
			//Debug.Log(success);
			onFloatArrayAccepted.Invoke(floatResults);
		}
	}

	/// <summary>
	/// Invoked when a device is connected.
	/// </summary>
	/// <param name="s">S.</param>
	public void OnConnect(string s){
		if(msg!=null)	msg.text = "Device connected!";
		Debug.Log("OnConnect()");
		onConnect.Invoke();
	}

	/// <summary>
	/// Invoked when the device is disconnected.
	/// </summary>
	/// <param name="s">S.</param>
	public void OnDisconnect(string s){
		if(msg!=null)	msg.text = "Device disconnected!";
		Debug.Log("OnDisconnect()");
		onDisconnect.Invoke();
	}
}
