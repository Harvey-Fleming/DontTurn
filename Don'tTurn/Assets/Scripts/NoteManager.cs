using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour
{
    [TextArea(10, 1000)]
    public string[] notesText;

    public bool[] notesObtained;
    public TextMeshProUGUI text;
    int noteCount = 0;

    public void AddToNoteCount()
    {
        noteCount++;
        text.text = noteCount.ToString() + "/9";
    }
}
