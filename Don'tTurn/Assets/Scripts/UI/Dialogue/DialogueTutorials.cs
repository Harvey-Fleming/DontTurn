using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTutorials : MonoBehaviour
{
    DialogueManager dialogueManager;
    DialogueTrigger dialogueTrigger;
    public GameObject tutorialPicture;
    public GameObject pressEscapeText; 
    bool isPictureUp;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueTrigger.isTalking == true)
        {
            if(dialogueManager.isEndingDialogue == true)
            {
                StartCoroutine(StartCooldown()); 
                //dialogueManager.animator.SetBool("isOpen", false);
               
            }
        }
       if(isPictureUp == true && Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1f;
            tutorialPicture.SetActive(false);
            isPictureUp = false;
            dialogueManager.textIsActive = false;
            dialogueManager.canDisplayNextSentence = false;
            playerMovement.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            playerMovement.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            playerMovement.enabled = true;
            pressEscapeText.SetActive(true);
        }

    }

    public void PicturePopUp()
    {
        Time.timeScale = 0; 
        tutorialPicture.SetActive(true);
        StartCoroutine(Cooldown()); 
       
    }


    public IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(1f);
        pressEscapeText.SetActive(true);
        isPictureUp = true;
    }

    public IEnumerator StartCooldown()
    {
        yield return new WaitForSecondsRealtime(1f);
        PicturePopUp();
    }
}
