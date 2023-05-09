using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceChangeTrigger : MonoBehaviour
{
    public AudioManager audioManager;

    [Header("Surface")]
    [SerializeField] private FootstepTypes surface;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioManager.SetFootstepsSurface(surface);
        }
    }
}
