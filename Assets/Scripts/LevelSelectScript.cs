using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
	public Animator crossfade;

	public void OpenLevelSelect()
	{
		gameObject.SetActive(true);
	}

	public void CloseLevelSelect()
	{
		gameObject.SetActive(false);
	}

    public void StartIntroduction()
	{
		LoadLevel("MainMenu"); //ToDo Introduction Scene
	}

	public void StartLevel1()
	{
		LoadLevel("Level-1");
	}

	public void StartLevel2()
	{
		LoadLevel("Level-2");
	}

	public void StartLevel3()
	{
		LoadLevel("Level-3");
	}

	public void StartLevel4()
	{
		LoadLevel("Level-4");
	}

	public void StartLevel5()
	{
		LoadLevel("Level-5");
	}

	public void StartLevel6()
	{
		LoadLevel("Level-6");
	}

	public void StartLevel7()
	{
		LoadLevel("Level-7");
	}

	public void StartEnding()
	{
		LoadLevel("Ending");
	}

	private void LoadLevel(string _target)
	{
		StartCoroutine(TransiotionLevel(_target));
	}

	private IEnumerator TransiotionLevel(string _target)
	{
		crossfade.gameObject.SetActive(true);
		crossfade.SetTrigger("startCrossfade");

		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(_target);
	}
}