using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Camera maincamera;
    private Vector2 mousePos;
    public Vector2 newCursorPos;

    private void Start() 
    {
        Cursor.visible = false;
    }

    private void Update() 
    {
        mousePos = Input.mousePosition;

        newCursorPos = maincamera.ScreenToWorldPoint(new Vector3 (mousePos.x,mousePos.y, 0f));

        transform.position = newCursorPos;

    }
}
