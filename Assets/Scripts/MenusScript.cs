using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
	public GameObject player;

	// Main Menu
	public void StartGame()
	{
		SceneManager.LoadScene("Level-1");
	}

    public void QuitGame()
	{
		Application.Quit();
	}

	// Pause Menu
	public void OpenPauseMenu()
	{
		gameObject.SetActive(true);
	}

	public void ResumeGame()
	{
		player.GetComponent<CharacterMain>().bPauseMenu = false;
		gameObject.SetActive(false);
	}

	public void RestartLevel()
	{
		Scene _activeScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(_activeScene.name);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}