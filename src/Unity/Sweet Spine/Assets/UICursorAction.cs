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

	/// <summary>
	/// Called whenever the gvr reticle pointer exits an object
	/// </summary>
	/// <param name="cursorAction">Cursor action.</param>
	/// <param name="gameObject">Game object.</param>
	void OnReticlePointerExit (CursorAction cursorAction, GameObject gameObject)
	{
		this.GetComponent<Text> ().text = "";
	}

	/// <summary>
	/// Called whenever the gvr reticle pointer enter an object
	/// </summary>
	/// <param name="cursorAction">Cursor action.</param>
	/// <param name="gameObject">Game object.</param>
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
