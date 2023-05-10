using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CureNPC : MonoBehaviour
{
    [Range(0,2)] public int cureGathered;
    [SerializeField] private Dialogue finalDialogue;
    [SerializeField] private CureManager cureManager;
    public GameObject toolIcons;

    private DialogueTrigger dialogueTrigger;
    public CureChamberCode cureChamber;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        cureManager = FindObjectOfType<CureManager>();
        toolIcons.SetActive(false);
    }

    public void CheckWin()
    {
        if(cureGathered == 2)
        {
            cureChamber.Fix();
        }
        else
        {
            return;
        }
        
    }

    public void StartDialogue()
    {
        toolIcons.SetActive(true);
        cureGathered = cureManager.cureAmount;
        dialogueTrigger.dialogue.sentences[0] = finalDialogue.sentences[cureGathered];
    }
}
