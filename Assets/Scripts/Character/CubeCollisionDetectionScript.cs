using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionDetectionScript : MonoBehaviour
{
    GameObject player;
    bool canCheck=true;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player.GetComponent<CharacterMain>().isCircle && canCheck)
        {
            player.GetComponent<CharacterMain>().EmitSoundBounce();
            canCheck = false;
            StartCoroutine(DelayBeforeCollisionCheck(2f));
        }
    }
   
    IEnumerator DelayBeforeCollisionCheck(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canCheck = true;
    }
}
