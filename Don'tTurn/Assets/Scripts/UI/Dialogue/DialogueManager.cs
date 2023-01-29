using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class DialogueManager : MonoBehaviour
{
    //this script manages 
    public Animator animator; 
    public TextMeshProUGUI name;
    public TextMeshProUGUI dialogueText; 

    private Queue<string> sentences; 
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            DisplayNextSentence(); 
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("isOpen", true); 
        Debug.Log("Starting conversation with" + dialogue.name);

        name.text = dialogue.name;

        sentences.Clear(); 

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return; 
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence; 
    }


    public void EndDialogue()
    {
        Debug.Log("End of COnversation");
        animator.SetBool("isOpen", false);
    }
}
