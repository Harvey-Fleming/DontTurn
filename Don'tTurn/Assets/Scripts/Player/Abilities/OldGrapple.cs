using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class OldGrapple : MonoBehaviour
{
    //Component / Object references
    private GameObject enemyObj;
    private DistanceJoint2D joint2D;
    private LineRenderer grappleLine;
    private Rigidbody2D rb2D;

    //Script References
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    [SerializeField] private Cursor cursorScript;
    private CorruptionScript corruptionScript;

    [Header("Hook Variables")]
    [SerializeField] private float hookRange = 5f;
    [SerializeField] private float enemySlerpSpeed = 4f;
    [SerializeField] private float grappleCooldown = 1f;
    [SerializeField] private float maxswingSpeed = 10f, swingAcceleration = 1;
    private float hookAngle, IndicatorlerpPercent;
    private Vector3 mouseWorldPos, grapplePointPos, hookDirection, jumpVelocity;

    [Header("Hook States")]
    public bool isUnlocked = false;
    [SerializeField] private bool isGrappling = false;
    [SerializeField] private bool isEnemyGrappled = false;
    [SerializeField] private bool canGrapple = true;

    private void Awake() 
    {
        cursorScript = GameObject.Find("Cursor").GetComponent<Cursor>();
        corruptionScript = GameObject.FindObjectOfType<CorruptionScript>();
        grappleLine = GetComponent<LineRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
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
            //Draws a line from the player towards the cursor but stops at max distance(Hook Range) to show the player how far away they can hook from
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
                playerMovement.ResetAirJump();
            }
            else if(playerInput.moveRight)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x + swingAcceleration, rb2D.velocity.y);
                //rb2D.velocity = Vector2.ClampMagnitude(new Vector2(Mathf.Lerp(rb2D.velocity.x, maxswingSpeed, 4 * Time.deltaTime), rb2D.velocity.y), maxswingSpeed);
                //transform.Translate(new Vector3(1 * swingSpeed * Time.deltaTime,0,0));
            }
            else if(playerInput.moveLeft)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x + -swingAcceleration, rb2D.velocity.y);
                //rb2D.velocity = new Vector2(rb2D.velocity.x + Mathf.Lerp(-swingSpeed, -maxswingSpeed, 4 * Time.deltaTime), rb2D.velocity.y );
                //transform.Translate(new Vector3(-1 * swingSpeed * Time.deltaTime,0,0));
            }
            rb2D.velocity = Vector2.ClampMagnitude(rb2D.velocity, maxswingSpeed);
        }

        if(isEnemyGrappled)
        {
            if  (Vector3.Distance(enemyObj.transform.position, transform.position) > 3)
            {
                enemyObj.transform.position = Vector3.Slerp(enemyObj.transform.position, transform.position, Time.deltaTime * enemySlerpSpeed);
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
        
        if (playerInput.meleeInput && joint2D == null && grapplehit.collider != null && canGrapple)
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

        joint2D = gameObject.AddComponent<DistanceJoint2D>();
        joint2D.autoConfigureConnectedAnchor = false;
        joint2D.autoConfigureDistance = false;
        joint2D.connectedAnchor = grapplePointPos;

        joint2D.distance = Vector3.Distance(grapplePointPos, transform.position);

        //Disable player movement apart from jump
    }

    private void StopGrapple()
    {
        Destroy(joint2D);
        isGrappling = false;
        grappleLine.positionCount = 0;
        playerMovement.enabled = true;
        StartCoroutine("Cooldown");
    }

    IEnumerator Cooldown()
    {
        canGrapple = false;
        yield return new WaitForSeconds(grappleCooldown);
        canGrapple = true;
        yield break;
    }

    private void DrawLine(Vector3 Pos1, Vector3 Pos2)
    {
        grappleLine.enabled = true;
        grappleLine.positionCount = 2;

        grappleLine.SetPosition(0, Pos1);
        grappleLine.SetPosition(1, Pos2);
    }

}




