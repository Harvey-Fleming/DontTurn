using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Transform noteGraphic;
    float y;
    float startY;
    public float bobSpeed;
    public float bobAmount;
    public int noteNum;
    public NoteManager noteManager;

    private void Start()
    {
        startY = noteGraphic.position.y;
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
            Destroy(gameObject);
        } 
    }
}
