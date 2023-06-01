using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnClipping : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            GameObject.Find("Player").GetComponent<PlayerStats>().StopIFrames();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameObject.Find("Player").GetComponent<PlayerStats>().StartIFrames();
        }
    }
}
