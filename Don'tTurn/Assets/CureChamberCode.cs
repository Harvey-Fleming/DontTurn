using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureChamberCode : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject UI;
    public SpriteRenderer spriteRend;
    public Sprite[] chamberPhases;
    public CureNPC cureNPC;
    bool isFixed = false;
    bool canInteract = false;
    public bool zoomOnChamber;
    public GameObject lights;
    public FollowCamera followCam;
    bool hasNotInteracted = true;

    public void Fix()
    {
        spriteRend.sprite = chamberPhases[1];
        lights.SetActive(true);
        isFixed = true;
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.W) && hasNotInteracted)
        {
            StartCoroutine(Open());
            UI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isFixed)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isFixed)
        {
            canInteract = false;
        }
    }

    IEnumerator Open()
    {
        zoomOnChamber = true;
        hasNotInteracted = true;
        playerMovement.enabled = false;
        followCam.Shake();
        GetComponent<BoxCollider2D>().enabled = false;
        spriteRend.sprite = chamberPhases[2];
        yield return new WaitForSeconds(1.5f);
        playerMovement.GetComponent<SpriteRenderer>().enabled = false;
        spriteRend.sprite = chamberPhases[1];
    }
}
