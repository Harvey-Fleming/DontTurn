using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAbility : MonoBehaviour
{
    [SerializeField] private Cursor cursorScript;
    private SpringJoint2D joint2D;
    private PlayerMovement playerMovement;
    private LineRenderer grappleLine;
    public bool isUnlocked = false, isGrappling = false;

    private float hookRange = 10f, hookAngle;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopGrapple();
            }
        }
    }

    private void FindGrapplePoint()
    {
        hookAcceptanceRadius = new Vector2(5,5);
        hookDirection = mouseWorldPos - transform.position;
        hookAngle = Mathf.Atan2(hookDirection.y, hookDirection.x);
        RaycastHit2D grapplehit = Physics2D.Raycast(gameObject.transform.position, hookDirection, hookRange);
        Debug.Log(grapplehit.collider);
        
        if (Input.GetMouseButtonDown(0) && joint2D == null)
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
                OnGrapple();
                Debug.Log("Grappled Point");
            }
            else if (Input.GetMouseButtonDown(0) && joint2D != null)
            {
                StopGrapple();
            }
        }

        //TODO - Display distance to player somehow
    }

    private void OnEnemyGrapple()
    {
        playerMovement.enabled = false;
        //Line renderer
        //Disable player movement apart from jump
    }

    private void OnGrapple()
    {
        playerMovement.enabled = false;

        joint2D = gameObject.AddComponent<SpringJoint2D>();
        joint2D.autoConfigureConnectedAnchor = false;
        joint2D.autoConfigureDistance = false;
        joint2D.connectedAnchor = grapplePointPos;

        joint2D.distance = Vector3.Distance(grapplePointPos, transform.position) - 5;

        joint2D.dampingRatio = 0;
        joint2D.frequency = 17;

        //Disable player movement apart from jump
    }

    private void StopGrapple()
    {
        Destroy(GetComponent<SpringJoint2D>());
        isGrappling = false;
        grappleLine.positionCount = 0;
        playerMovement.enabled = true;
    }

    private void DrawLine()
    {
        grappleLine.enabled = true;
        grappleLine.positionCount = 2;

        grappleLine.SetPosition(0, transform.position);
        grappleLine.SetPosition(1, grapplePointPos);
    }

    private void OnValidate() {
        grappleLine = GetComponent<LineRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

}




