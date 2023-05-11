using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public bool isChoiceNPC; //checks if the NPC has a choice; 
    public bool isFinalNPC;
    public Dialogue dialogue;
    public Button[] choiceButtons;
    public GameObject buttonToPress;
    private GameObject currentDialogueNPC;
    private CureNPC cureNPCScript;
    private bool isActive;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("In Trigger");
            isActive = true;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false; 
    }

    private void Update()
    {
        if(isActive == true && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Pressed W");
            currentDialogueNPC = this.gameObject;
            TriggerDialogue();
        }
    }


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().isChoiceNPC = isChoiceNPC;
        FindObjectOfType<DialogueManager>().isFinalNPC = isFinalNPC;
        if(isChoiceNPC == true)
        {
            FindObjectOfType<DialogueManager>().choiceButtons  = choiceButtons;
        }
        if (isFinalNPC)
        {
            cureNPCScript = GetComponent<CureNPC>();
            if(cureNPCScript.cureGathered == 2)
            {
                cureNPCScript.CheckWin();
            }
            cureNPCScript.StartDialogue();
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        currentDialogueNPC.GetComponent<UnlockAbility>()?.OnAbilityUnlock();
    }    

}
