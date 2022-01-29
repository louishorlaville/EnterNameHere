using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonInteraction : MonoBehaviour
{
    public bool moveAway = false;
    public bool setActive = false;

    public virtual void ButtonPressed()
    {
		if(moveAway == true)
		{
            gameObject.transform.position = new Vector3(1000f, 1000f, 0f);
		}

		if(setActive == true)
		{
			gameObject.SetActive(true);
		}
    }
}
