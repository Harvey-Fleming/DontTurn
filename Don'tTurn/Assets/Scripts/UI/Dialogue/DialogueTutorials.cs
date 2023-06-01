using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTutorials : MonoBehaviour
{
    DialogueManager dialogueManager;
    DialogueTrigger dialogueTrigger;
    public GameObject tutorialPicture;
    GameObject interactKey;
    [HideInInspector] public bool isPictureUp;
    private PlayerInput playerInput;
    bool hasPressed;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        interactKey = tutorialPicture.transform.Find("F").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueTrigger.isTalking == true)
        {
            if(dialogueManager.isEndingDialogue == true && !hasPressed)
            {
                dialogueTrigger.enabled = false;
                playerInput.enabled = false;
                dialogueManager.EndDialogue();
                StartCoroutine(StartCooldown()); 
                //dialogueManager.animator.SetBool("isOpen", false);
            }
        }
       if(isPictureUp == true && Input.GetKeyDown(KeyCode.F))
       {
            StartCoroutine(DisableDialogueManager());
            Time.timeScale = 1f;
            tutorialPicture.SetActive(false);
            isPictureUp = false;
            dialogueManager.textIsActive = false;
            dialogueManager.canDisplayNextSentence = false;
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
        yield return new WaitForSecondsRealtime(3f);
        interactKey.SetActive(true);
        isPictureUp = true;
    }

    public IEnumerator StartCooldown()
    {
        hasPressed = true;
        yield return new WaitForSecondsRealtime(0.2f);
        PicturePopUp();
    }

    IEnumerator DisableDialogueManager()
    {
        yield return new WaitForSeconds(0.1f);
        playerInput.enabled = true;
        dialogueTrigger.enabled = true;
    }
}
