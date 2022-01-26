using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject holeCollider;
    public GameObject spinnerL;
    public GameObject spinnerR;

    private void OnTriggerEnter2D(Collider2D _target)
	{
        if(_target.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

            StartCoroutine(EndLevelCoroutine(_target));
        }
    }

    IEnumerator EndLevelCoroutine(Collider2D _target)
	{
        float _elapsedTime = 0;
        float _waitTime = 3;

        _target.GetComponent<CharacterMain>().bEndLevel = true;
        _target.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        _target.GetComponent<Rigidbody2D>().gravityScale = 0f;

        while(_elapsedTime < _waitTime)
		{
            _target.transform.position = Vector2.Lerp(_target.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), (_elapsedTime / _waitTime));
            _target.transform.rotation = Quaternion.Lerp(_target.transform.rotation, new Quaternion(0f, 0f, 0f, 0f), (_elapsedTime * 0.25f));

            spinnerL.transform.rotation = Quaternion.Lerp(spinnerL.transform.rotation, _target.transform.rotation, (_elapsedTime * 0.25f));
            spinnerR.transform.rotation = Quaternion.Lerp(spinnerR.transform.rotation, _target.transform.rotation, (_elapsedTime * 0.25f));

            _elapsedTime += Time.deltaTime;
            
            yield return null;
		}

        _target.GetComponent<CharacterMain>().SwitchToSquare();

        holeCollider.SetActive(false);

        _target.GetComponent<Rigidbody2D>().gravityScale = 10f;

        yield return null;
    }
}