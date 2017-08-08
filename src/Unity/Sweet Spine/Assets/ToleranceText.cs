using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToleranceText : MonoBehaviour {
	public Slider slider;
	public float maxValue = 100.0f;
	public float value = 0.0f;

	public void OnSliderValueChange(GameObject slider)
	{
		this.gameObject.GetComponent<InputField>().text = slider.GetComponent<Slider>().value.ToString ("F");
		float value = float.Parse (this.gameObject.GetComponent<InputField> ().text);
		this.value = Mathf.Clamp (value, 0.0f, maxValue);
	}

	void OnEndEdit (string input)
	{
		float value = float.Parse(input);
		value = Mathf.Clamp (value, 0.0f, maxValue);
		this.value = value;
		slider.GetComponent<Slider> ().value = this.value;
	}

	void Start()
	{
		this.gameObject.GetComponent<InputField> ().text = 0.0f.ToString ("F");
		this.gameObject.GetComponent<InputField>().onEndEdit.AddListener (OnEndEdit);
	}
}
