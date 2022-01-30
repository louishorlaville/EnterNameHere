using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public Animator crossfade;
    public AK.Wwise.Event Princess;
    public AK.Wwise.Event Fall;
    public AK.Wwise.Event Squish;
    
    public void StartTransition()
	{
        StartCoroutine(TransiotionCoroutine());
    }

    private IEnumerator TransiotionCoroutine()
    {
        crossfade.SetTrigger("startCrossfade");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("CreditsVideo");
    }

    public void FallSound()
    {
        Fall.Post(gameObject);
    }
    public void SquishSound()
    {
        Squish.Post(gameObject);
    }
    public void PrincessSound()
    {
        Princess.Post(gameObject);
    }
}