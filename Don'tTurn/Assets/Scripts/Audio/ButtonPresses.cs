using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPresses : MonoBehaviour
{
    public void OnButtonClick()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }
}
