using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CureNPC : MonoBehaviour, IDataPersistence
{
    [Range(0,2)] public int cureGathered;
    [SerializeField] private Dialogue firstDialogue;
    [SerializeField] private Dialogue secondDialogue;
    [SerializeField] private Dialogue secondDialogueAfter;
    [SerializeField] private Dialogue thirdDialogue;
    [SerializeField] private Dialogue thirdDialogueAfter;
    [SerializeField] private CureManager cureManager;
    public GameObject toolIcons;
    [SerializeField] private bool hasTalkedTo;

    private DialogueTrigger dialogueTrigger;
    public CureChamberCode cureChamber;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        cureManager = FindObjectOfType<CureManager>();
        toolIcons.SetActive(false);

        if(hasTalkedTo)
        {
            toolIcons.SetActive(true);
        }

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

        if (cureGathered == 0)
        {
            dialogueTrigger.dialogue.sentences = firstDialogue.sentences;
        }
        if (cureGathered == 1 && !hasTalkedTo)
        {
            dialogueTrigger.dialogue.sentences = firstDialogue.sentences.Concat(secondDialogueAfter.sentences).ToArray();
        }
        else if (cureGathered == 1)
        {
            dialogueTrigger.dialogue.sentences = secondDialogue.sentences;
        }
        if (cureGathered == 2 && !hasTalkedTo)
        {
            dialogueTrigger.dialogue.sentences = firstDialogue.sentences.Concat(thirdDialogueAfter.sentences).ToArray();
        }
        else if (cureGathered == 2)
        {
            dialogueTrigger.dialogue.sentences = thirdDialogue.sentences;
        }

        hasTalkedTo = true;
    }

    public void SaveData(GameData data)
    {
        data.hasTalkedToFinalNPC = this.hasTalkedTo;
    }

    public void LoadData(GameData data)
    {
        this.hasTalkedTo = data.hasTalkedToFinalNPC;
    }
}
