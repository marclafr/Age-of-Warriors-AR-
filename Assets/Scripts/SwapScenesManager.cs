using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScenesManager : MonoBehaviour
{
	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}