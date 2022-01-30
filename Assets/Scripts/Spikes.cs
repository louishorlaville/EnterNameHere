using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
   
{
    public AK.Wwise.Event SpikeDeath;
    private void OnCollisionEnter2D(Collision2D _target)
	{
		if(_target.transform.tag == "Player" && _target.gameObject.GetComponent<CharacterMain>().isCircle == true)
    
		{
            SpikeDeath.Post(gameObject);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}