using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAbility : MonoBehaviour
{
    [SerializeField] private Cursor cursorScript;
    private LineRenderer grappleLine;
    public bool isUnlocked = false, isGrappling = false;

    private float hookRange = 200f, hookAngle;
    private Vector2 hookAcceptanceRadius, hookDirection;
    private Vector3 mouseWorldPos, grapplePointPos;


    private void Awake() 
    {
        cursorScript = GameObject.Find("Cursor").GetComponent<Cursor>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = cursorScript.newCursorPos;
        if(isUnlocked)
        {
           if (Input.GetMouseButton(1))
           {
            FindGrapplePoint();
           } 
        }

        if(isGrappling)
        {
            DrawLine();
        }
    }

    private void FindGrapplePoint()
    {
        hookAcceptanceRadius = new Vector2(5,5);
        hookDirection = mouseWorldPos - transform.position;
        hookAngle = Mathf.Atan2(hookDirection.y, hookDirection.x);
        RaycastHit2D grapplehit = Physics2D.Raycast(gameObject.transform.position, hookDirection, hookRange);
        Debug.Log(grapplehit.collider);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (grapplehit.collider.tag == "Enemy")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                isGrappling = true;
                DrawLine();
                //OnEnemyGrapple();
                Debug.Log("Grappled Enemy");
            }
            else if (grapplehit.collider.tag == "GrapplePoint")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                isGrappling = true;
                DrawLine();
                //OnGrapple();
                Debug.Log("Grappled Point");
            }
        }

        //TODO - Display distance to player somehow
    }

    private void OnEnemyGrapple()
    {
        DrawLine();
        //TODO - Line Render from player pos to hook.
        //Slerp? to point?
        //Disable player movement apart from jump
    }

    private void OnGrapple()
    {
        DrawLine();
        //TODO - Line Render from player pos to hook.
        //Slerp? to point?
        //Disable player movement apart from jump
    }

    private void DrawLine()
    {
        grappleLine.enabled = true;

        grappleLine.SetPosition(0, transform.position);
        grappleLine.SetPosition(1, grapplePointPos);
    }

    private void OnDrawGizmosSelected() {
        Color rayColour = Color.blue;
        Debug.DrawRay(gameObject.transform.position, hookDirection, rayColour);
    }

    private void OnValidate() {
        grappleLine = GetComponent<LineRenderer>();
    }

}




