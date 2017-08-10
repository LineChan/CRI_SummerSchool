using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuItemUI : MonoBehaviour {
	public CanvasGroup mainMenu;
	public CanvasGroup calibrateMenu;
	public string firstLevelName;

	public void OnCalibrateClick()
	{
		ShowCanvasGroup (calibrateMenu);
		HideCanvasGroup (mainMenu);
	}

	public void OnQuitClick()
	{
		Application.Quit();
	}

	public void OnBackClick()
	{
		ShowCanvasGroup (mainMenu);
		HideCanvasGroup (calibrateMenu);
	}

	void ShowCanvasGroup(CanvasGroup canvasGroup)
	{
		canvasGroup.alpha = 1;
		canvasGroup.blocksRaycasts = true;
		canvasGroup.interactable = true;
	}

	void HideCanvasGroup(CanvasGroup canvasGroup)
	{
		canvasGroup.alpha = 0;
		canvasGroup.blocksRaycasts = false;
		canvasGroup.interactable = false;
	}

	public void OnPlayClick()
	{
		SceneManager.LoadScene (firstLevelName);
	}
}
