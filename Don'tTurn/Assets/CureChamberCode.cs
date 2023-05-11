using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    public float lightIntensity = 1f;
    float elapsedTime;
    public float timeToIntes;
    public Light2D light1;
    public Light2D light2;
    public bool startLights;
    public float maxIntensity;
    public float startIntensity;
    public GameObject interactKey;

    private void Start()
    {
        interactKey.SetActive(false);
    }

    public void Fix()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ChamberRepair, this.transform.position);
        spriteRend.sprite = chamberPhases[1];
        interactKey.SetActive(true);
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

        if (startLights)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / timeToIntes;

            light1.intensity = Mathf.Lerp(startIntensity, maxIntensity, percent);
            light2.intensity = Mathf.Lerp(startIntensity, maxIntensity, percent);
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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.EndingSFX, this.transform.position);
        zoomOnChamber = true;
        hasNotInteracted = true;
        playerMovement.enabled = false;
        playerMovement.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.playerFootsteps.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        followCam.Shake();
        GetComponent<BoxCollider2D>().enabled = false;
        spriteRend.sprite = chamberPhases[2];
        yield return new WaitForSeconds(1.5f);
        playerMovement.GetComponent<SpriteRenderer>().enabled = false;
        spriteRend.sprite = chamberPhases[1];
    }
}
