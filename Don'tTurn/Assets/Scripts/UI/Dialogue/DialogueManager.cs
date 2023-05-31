using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using FMOD.Studio;

public class DialogueManager : MonoBehaviour
{
    public Button[] choiceButtons;
    public Animator animator; 
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText; 
    [SerializeField] private GameObject textboxObj;
    [SerializeField] private PlayerMovement playerMovement;
    private CureNPC cureNPC;
    public bool isChoiceNPC; //checks if the NPC has a choice; 
    public bool isFinalNPC;
    public bool canDisplayNextSentence = false;
    public bool textIsActive;
    public PauseMenu pause;
    public bool canStartDialogue = true;
    public bool isEndingDialogue;
    public DialogueTutorials dialogueTutorial;

    private Queue<string> sentences; 

    private void Awake() 
    {
        textboxObj.SetActive(true);
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
        cureNPC = FindObjectOfType<CureNPC>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canDisplayNextSentence && !dialogueTutorial.isPictureUp)
        {
            DisplayNextSentence();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && textIsActive)
        {
            StartCoroutine(DialogueExitCooldown());
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isEndingDialogue = false;
        canStartDialogue = false;
        textIsActive = true;
        playerMovement.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerMovement.MakeIdle(playerMovement.gameObject.GetComponent<Rigidbody2D>().velocity.x);
        playerMovement.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        playerMovement.enabled = false;
        playerMovement.playerFootsteps.stop(STOP_MODE.IMMEDIATE);
        animator.SetBool("isOpen", true); 

        nameText.text = dialogue.name;

        sentences.Clear(); 

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        canDisplayNextSentence = false;

        DisplayNextSentence();
        StartCoroutine(DialogueCooldown());
    }

    public void DisplayNextSentence()
    {
        canDisplayNextSentence = false;
        StartCoroutine(DialogueCooldown());

        if(sentences.Count == 0)
        {
            if(isChoiceNPC == true)
            {
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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence; 
    }


    public void EndDialogue()
    {
        if (canStartDialogue == false)
        {
            StartCoroutine(StartDialogueCooldown());
        }
        playerMovement.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        playerMovement.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
        if(isFinalNPC)
        {
            cureNPC.CheckWin();
        }
        animator.SetBool("isOpen", false);
        textIsActive = false;
        canDisplayNextSentence = false;
        isEndingDialogue = true;
        StartCoroutine(StopEndDialogueBool());
    }

    IEnumerator DialogueCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        canDisplayNextSentence = true;
    }

    IEnumerator DialogueExitCooldown()
    {
        yield return new WaitForEndOfFrame();
        EndDialogue();
        isEndingDialogue = false; 
        canDisplayNextSentence = false;
    }

    IEnumerator StartDialogueCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        isEndingDialogue = false; 
        canStartDialogue = true;
    }

    IEnumerator StopEndDialogueBool()
    {
        yield return new WaitForSeconds(1f);
        isEndingDialogue = false;
    }
}
