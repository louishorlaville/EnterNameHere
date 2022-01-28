using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
	public Animator crossfade;
	public bool lastLevel = false;

	private void OnTriggerEnter2D(Collider2D _target)
	{
		if(_target.gameObject.tag == "Player")
		{
			LoadNextLevel();
		}
	}

	private void LoadNextLevel()
	{
		int _newIndex = SceneManager.GetActiveScene().buildIndex + 1;

		StartCoroutine(TransiotionNextLevel(_newIndex));
	}

	IEnumerator TransiotionNextLevel(int _index)
	{
		crossfade.SetTrigger("startCrossfade");

		yield return new WaitForSeconds(1f);

		if(lastLevel == false)
		{
			SceneManager.LoadScene(_index);
		}
		else
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}