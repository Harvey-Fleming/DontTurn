using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject buttonToPress;
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
        if(isActive == true && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue(); 
        }
    }


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }    
}
