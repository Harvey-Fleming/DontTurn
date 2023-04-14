using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    bool flashlightEnabled;
    public GameObject flashlight;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashlightEnabled = !flashlightEnabled;
        }

        if (flashlightEnabled)
        {
            flashlight.SetActive(true);
        }
        if (!flashlightEnabled)
        {
            flashlight.SetActive(false);
        }
    }
}
