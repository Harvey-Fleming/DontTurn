using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour, IDataPersistence
{
    public Transform noteGraphic;
    float y;
    float startY;
    public float bobSpeed;
    public float bobAmount;
    public int noteNum;
    public NoteManager noteManager;
    public bool hasCollectedNote;

    private void Start()
    {
        startY = noteGraphic.position.y;

        if(hasCollectedNote == true)
        {
            noteManager.notesObtained[noteNum] = true;
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        y = startY + (Mathf.Sin(Time.time * bobSpeed) * bobAmount/1000f);

        noteGraphic.position = new Vector3(noteGraphic.position.x, y, noteGraphic.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            noteManager.notesObtained[noteNum] = true;
            hasCollectedNote = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.NotePickup, this.transform.position);
            Destroy(gameObject);
        } 
    }

    #region SaveRegion
    public void SaveData(GameData data)
    {
        if(data.hasCollectedNote.ContainsKey(noteNum))
        {
            data.hasCollectedNote.Remove(noteNum);
        }
        data.hasCollectedNote.Add(noteNum, hasCollectedNote);
    }

    public void LoadData(GameData data)
    {
        data.hasCollectedNote.TryGetValue(noteNum, out hasCollectedNote);
    }
    #endregion
}
