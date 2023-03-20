using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Camera maincamera;
    private Vector2 mousePos;
    public Vector2 newCursorPos;

    private void Update() 
    {
        mousePos = Input.mousePosition;

        newCursorPos = maincamera.ScreenToWorldPoint(mousePos);

        transform.position = newCursorPos;

    }
}
