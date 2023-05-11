using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    public Button mapButton;
    public Button notesButton;
    public GameObject mapPanel;
    public GameObject notesPanel;
    public NoteManager noteManager;
    public bool isOpen;
    public TextMeshProUGUI noteText;
    public Button left;
    public Button right;
    int pageNum = 0;
    public string unknownNote;
    public Image screwdriverCircle;
    public Image wrenchCircle;
    public GameObject screwDriver;
    public GameObject wrench;
    public Sprite redX;

    void Start()
    {
        mapButton.interactable = false;
        notesButton.interactable = true;
        mapPanel.SetActive(true);
        notesPanel.SetActive(false);
    }

    public void ClickMaps()
    {
        mapPanel.SetActive(true);
        notesPanel.SetActive(false);
        mapButton.interactable = false;
        notesButton.interactable = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.notebookTurn, this.transform.position);
    }

    public void ClickNotes()
    {
        notesPanel.SetActive(true);
        mapPanel.SetActive(false);
        notesButton.interactable = false;
        mapButton.interactable = true;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.notebookTurn, this.transform.position);
    }

    private void Update()
    {
        if(screwDriver == null)
        {
            screwdriverCircle.color = Color.white;
            screwdriverCircle.sprite = redX;
        }

        if(wrench == null)
        {
            wrenchCircle.color = Color.white;
            wrenchCircle.sprite = redX;
        }

        if (isOpen)
        {
            if (noteManager.notesObtained[pageNum])
            {
                noteText.text = noteManager.notesText[pageNum];
            }
            else
            {
                noteText.text = "Note #" + (pageNum + 1) + "\n\n" + unknownNote;
            }
        }

        if (pageNum <= 0)
        {
            left.interactable = false;
        }
        else
        {
            left.interactable = true;
        }

        if(pageNum >= noteManager.notesText.Length - 1)
        {
            right.interactable = false;
        }
        else
        {
            right.interactable = true;
        }
    }

    public void Right()
    {
        pageNum += 1;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.notebookTurn, this.transform.position);
    }

    public void Left()
    {
        pageNum -= 1;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.notebookTurn, this.transform.position);
    }
}
