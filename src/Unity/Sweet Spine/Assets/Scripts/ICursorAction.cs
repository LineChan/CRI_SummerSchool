using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorAction {
	Move,
	Pick,
	Use,
	Activate,
	PlaceItem,
	Custom,
	None,
}

static class CursorActionMethods {
	public static string ToRichTextString(this CursorAction action)
	{
		string res;
		switch (action) {
		case CursorAction.Activate:
			res =  "<color=orange>Activate</color>";
			break;
		case CursorAction.Move:
			res = "<color=white>Move</color>";
			break;
		case CursorAction.Use:
			res = "<color=orange>Use</color>";
			break;
		case CursorAction.Pick:
			res =  "<color=orange>Pick</color>";
			break;
		case CursorAction.PlaceItem:
			res = "<color=cyan>Place item</color>";
			break;
		default:
			res = "";
			break;
		}
		return res;
	}

	public static string ToString(this CursorAction action)
	{
		string res;
		switch (action) {
		case CursorAction.Activate:
			res = "Activate";
			break;
		case CursorAction.Move:
			res = "Move";
			break;
		case CursorAction.Use:
			res = "Use";
			break;
		case CursorAction.Pick:
			res = "Pick";
			break;
		case CursorAction.PlaceItem:
			res = "Place item";
			break;
		case CursorAction.Custom:
			res = "";
			break;
		default:
			res = "";
			break;
		}
		return res;
	}
}
/// <summary>
/// Interface for the Actions of the game. Each component that triggers an action for the reticle pointer should implement this interface.
/// The interface
/// </summary>
public interface ICursorAction {
	CursorAction action { get; }
	string customMessage { get; }
}
