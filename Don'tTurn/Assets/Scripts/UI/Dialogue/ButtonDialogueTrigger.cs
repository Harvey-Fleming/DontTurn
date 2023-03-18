using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ButtonDialogueTrigger : MonoBehaviour
{
    public bool isChoiceNPC; //checks if the NPC has a choice; 
    public Dialogue dialogue;
    private GameObject currentDialogueNPC;
    private bool isActive;
    DialogueParentScript dialogueParentScript; 

    private void Start()
    {
        dialogueParentScript = GetComponentInParent<DialogueParentScript>(); 
    }

    private void Update()
    {
            
    }


    public void TriggerDialogue()
    {
        
        currentDialogueNPC = this.gameObject;
        FindObjectOfType<DialogueManager>().isChoiceNPC = isChoiceNPC;
        dialogueParentScript.isPressed = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
       

    }
}


