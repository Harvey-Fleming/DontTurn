using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class DialogueManager : MonoBehaviour
{
    public Button[] choiceButtons;
    public Animator animator; 
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText; 
    [SerializeField] private GameObject textboxObj;
    private CureNPC cureNPC;
    public bool isChoiceNPC; //checks if the NPC has a choice; 
    public bool isFinalNPC;

    private Queue<string> sentences; 

    private void Awake() 
    {
        textboxObj.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
        cureNPC = FindObjectOfType<CureNPC>();
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

        nameText.text = dialogue.name;

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
            if(isChoiceNPC == true)
            {
                Debug.Log("Enters thingy!"); 
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    choiceButtons[i].gameObject.SetActive(true); 
                }
            }
            else
            {
                EndDialogue();
                return;
            }
           
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence; 
    }


    public void EndDialogue()
    {
        Debug.Log("End of Conversation");
        if(isFinalNPC)
        {
            cureNPC.CheckWin();
        }
        animator.SetBool("isOpen", false);
    }
}
