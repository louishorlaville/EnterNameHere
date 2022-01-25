using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject holeCollider;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D _target)
	{
        if(_target.tag == "Player")
        {
            Debug.Log("End");
            gameObject.GetComponent<Collider2D>().enabled = false;

            coroutine = EndLevelCoroutine(_target);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator EndLevelCoroutine(Collider2D _target)
	{
        Debug.Log("Coroutine");
        float _elapsedTime = 0;
        float _waitTime = 2;

        _target.GetComponent<CharacterMain>().bEndLevel = true;
        _target.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        _target.GetComponent<Rigidbody2D>().gravityScale = 0f;

        //yield return new WaitForSeconds(0.5f);

        while(_elapsedTime < _waitTime)
		{
            _target.transform.position = Vector2.Lerp(_target.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), (_elapsedTime / _waitTime));
            _target.transform.rotation = Quaternion.Lerp(_target.transform.rotation, new Quaternion(0f, 0f, 0f, 0f), (_elapsedTime * 0.25f));
            
            _elapsedTime += Time.deltaTime;
            
            yield return null;
		}

        _target.GetComponent<CharacterMain>().SwitchToSquare();

        holeCollider.SetActive(false);

        _target.GetComponent<Rigidbody2D>().gravityScale = 10f;

        //StopCoroutine(coroutine);
    }
}