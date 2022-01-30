using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
	public GameObject player;
	public Animator crossface;
	public GameObject buttons;

	// Main Menu
	public void StartGame()
	{
		buttons.GetComponent<Animator>().SetBool("hide", true);
		StartCoroutine("StartIntro");
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
		StartCoroutine(TransiotionLevel(_activeScene.name));
	}

	public void MainMenu()
	{
		StartCoroutine(TransiotionLevel("MainMenu"));
	}

	private IEnumerator TransiotionLevel(string _target)
	{
		crossface.gameObject.SetActive(true);
		crossface.SetTrigger("startCrossfade");

		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(_target);
	}

	private IEnumerator StartIntro()
    {
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("IntroVideo");
	}

}