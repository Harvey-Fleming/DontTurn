using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public bool isChoiceNPC; //checks if the NPC has a choice; 
    public Dialogue dialogue;
    public Button[] choiceButtons;
    public GameObject buttonToPress;
    private GameObject currentDialogueNPC;
    private bool isActive;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            buttonToPress.SetActive(true);
            isActive = true;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttonToPress.SetActive(false);
        isActive = false; 
    }

    private void Update()
    {
        if(isActive == true && Input.GetKeyDown(KeyCode.W))
        {
            currentDialogueNPC = this.gameObject;
            TriggerDialogue();
        }
    }


    public void TriggerDialogue()
    {
        if(isChoiceNPC == true)
        {

                FindObjectOfType<DialogueManager>().choiceButtons  = choiceButtons;
     
        }
        FindObjectOfType<DialogueManager>().isChoiceNPC = isChoiceNPC;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        currentDialogueNPC.GetComponent<UnlockAbility>()?.OnAbilityUnlock();
       
        
    }    

}
