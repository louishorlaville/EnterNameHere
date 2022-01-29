using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public GameObject InteractObject;
    Transform button;
    Vector3 PressedPosition;
    Vector3 UnpressedPosition;
    bool buttonIsPressed= false;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetChild(0);
        UnpressedPosition = button.transform.position;
        PressedPosition = new Vector3(button.transform.position.x, button.transform.position.y - 4f, button.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<CharacterMain>().isCircle == false && buttonIsPressed == false)
        {
            StartCoroutine("PressButton");
            buttonIsPressed = true;
        }
        
    }

    IEnumerator PressButton()
    {
        float elapsedTime = 0;
        float waitTime = 1;
        var currentPos = button.transform.position;

        while (elapsedTime < waitTime)
        {
            button.transform.position = Vector3.Lerp(currentPos, PressedPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        InteractObject.GetComponent<ButtonInteraction>().ButtonPressed();
        yield return null;
    }
}
