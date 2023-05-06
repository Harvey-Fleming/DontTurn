using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public GameObject flashlight;
    public BoxCollider2D flashlightCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == flashlightCollider)
        {
            flashlight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == flashlightCollider)
        {
            flashlight.SetActive(false);
        }
    }
}
