using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICursorAction : MonoBehaviour {
	public void OnEnable()
	{
		GvrReticlePointerImpl.onReticlePointerEnter += OnReticlePointerEnter;
		GvrReticlePointerImpl.onReticlePointerExit += OnReticlePointerExit;
	}

	void OnReticlePointerExit (CursorAction cursorAction, GameObject gameObject)
	{
		this.GetComponent<Text> ().text = "";
	}

	void OnReticlePointerEnter (CursorAction cursorAction, GameObject gameObject)
	{
		this.GetComponent<Text>().text = cursorAction.ToRichTextString ();
	}

	public void OnDisable()
	{
		GvrReticlePointerImpl.onReticlePointerEnter -= OnReticlePointerEnter;
		GvrReticlePointerImpl.onReticlePointerExit -= OnReticlePointerExit;
	}
}
