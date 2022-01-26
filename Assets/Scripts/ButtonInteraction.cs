using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonInteraction : MonoBehaviour
{
    public bool moveAway;

    public virtual void ButtonPressed()
    {
		if(moveAway == true)
		{
            gameObject.transform.position = new Vector3(1000f, 1000f, 0f);
		}
    }
}
