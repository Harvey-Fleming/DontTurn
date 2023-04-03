using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class GrappleAbility : MonoBehaviour
{
    [SerializeField] private Cursor cursorScript;
    private CorruptionScript corruptionScript;
    private GameObject enemyObj;
    private SpringJoint2D joint2D;
    private PlayerMovement playerMovement;
    private LineRenderer grappleLine;
    private PlayerInput playerInput;


    [SerializeField] private float hookRange = 5f;
    private float hookAngle, IndicatorlerpPercent, slerpSpeed = 4f;
    private Vector2 hookDirection;
    private Vector3 mouseWorldPos, grapplePointPos;
    public bool isUnlocked = false, isGrappling = false, isEnemyGrappled = false;

    private void Awake() 
    {
        cursorScript = GameObject.Find("Cursor").GetComponent<Cursor>();
        corruptionScript = GameObject.FindObjectOfType<CorruptionScript>();
        grappleLine = GetComponent<LineRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPos = cursorScript.newCursorPos;
        if(isUnlocked)
        {
           if (playerInput.moveAbilityInput && playerInput.grappleSelected)
           {
            IndicatorlerpPercent = (hookRange/Vector3.Distance(transform.position, mouseWorldPos));
            DrawLine(transform.position, Vector3.Lerp(transform.localPosition, mouseWorldPos, IndicatorlerpPercent));
            FindGrapplePoint();
           } 
           else if (playerInput.moveAbilityInputRelease)
           {
            grappleLine.positionCount = 0;
           }
        }

        if(isGrappling)
        {
            DrawLine(transform.position, grapplePointPos);
            if (playerInput.jumpKey)
            {
                StopGrapple();
            }
        }

        if(isEnemyGrappled)
        {
            if  (Vector3.Distance(enemyObj.transform.position, transform.position) > 3)
            {
                enemyObj.transform.position = Vector3.Slerp(enemyObj.transform.position, transform.position, Time.deltaTime * slerpSpeed);
            }
            else if (Vector3.Distance(enemyObj.transform.position, transform.position) < 3)
            {
                isEnemyGrappled = false;
                StopGrapple();
                return;
            }
        }
    }

    private void FindGrapplePoint()
    {
        hookDirection = mouseWorldPos - transform.position;
        RaycastHit2D grapplehit = Physics2D.Raycast(gameObject.transform.position, hookDirection, hookRange);
        Debug.Log(grapplehit.collider);
        
        if (playerInput.meleeInput && joint2D == null && grapplehit.collider != null)
        {
            if (grapplehit.collider.tag == "Enemy")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                isGrappling = true;
                DrawLine(transform.position, grapplePointPos);
                enemyObj = grapplehit.collider.gameObject;
                OnEnemyGrapple();
                Debug.Log("Grappled Enemy");
            }
            else if (grapplehit.collider.tag == "GrapplePoint")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                isGrappling = true;
                DrawLine(transform.position, grapplePointPos);
                OnGrapple();
                Debug.Log("Grappled Point");
            }
            else if (grapplehit.collider.tag == null)
            {
                return;
            }
            else if (playerInput.meleeInput && joint2D != null)
            {
                StopGrapple();
            }
        }
    }

    private void OnEnemyGrapple()
    {
        playerMovement.enabled = false;
        isEnemyGrappled = true;
        corruptionScript.OnReduceCorruption(15);
        //Disable player movement apart from jump
    }

    private void OnGrapple()
    {
        playerMovement.enabled = false;

        joint2D = gameObject.AddComponent<SpringJoint2D>();
        joint2D.autoConfigureConnectedAnchor = false;
        joint2D.autoConfigureDistance = false;
        joint2D.connectedAnchor = grapplePointPos;

        joint2D.distance = Vector3.Distance(grapplePointPos, transform.position);
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

    private void DrawLine(Vector3 Pos1, Vector3 Pos2)
    {
        grappleLine.enabled = true;
        grappleLine.positionCount = 2;

        grappleLine.SetPosition(0, Pos1);
        grappleLine.SetPosition(1, Pos2);
    }

}




