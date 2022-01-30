using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public Animator crossfade;

    public void StartTransition()
	{
        StartCoroutine(TransiotionCoroutine());
    }

    private IEnumerator TransiotionCoroutine()
    {
        crossfade.SetTrigger("startCrossfade");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainMenu");
    }
}