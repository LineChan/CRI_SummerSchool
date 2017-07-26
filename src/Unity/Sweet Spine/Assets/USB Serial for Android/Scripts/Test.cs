using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	[SerializeField] Text msg;
	[SerializeField] Text txtAccepted;
	[SerializeField] Text txtDeclined;

	int nAccepted;
	int nDeclined;

	// Use this for initialization
	void Start () {

	}

	public void OnSerialDataAccepted(string data){
		msg.text = data;
		nAccepted++;
		txtAccepted.text = nAccepted.ToString();
	}
	public void OnSerialDataAcceptedFloatArray(float[] data){
		if(data.Length == 3){
			Vector3 lea = new Vector3(data[0], data[1], data[2]);
			transform.localEulerAngles = lea;
		}
	}
	public void OnSerialDataDeclined(){
		nDeclined++;
		txtDeclined.text = nDeclined.ToString();
	}
}
