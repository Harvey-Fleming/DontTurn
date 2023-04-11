using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class GrappleAbility : MonoBehaviour
{
    //Component / Object references
    private GameObject enemyObj;
    private LineRenderer grappleLine;
    private Rigidbody2D rb2D;

    //Script References
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private Cursor cursorScript;
    private CorruptionScript corruptionScript;

    [Header("Hook Variables")]
    [SerializeField] private float hookRange = 5f;
    [SerializeField] private float enemySlerpSpeed = 4f, LerpSpeed = 8f;
    [SerializeField] private float grappleCooldown = 1f;

    private float hookAngle, IndicatorlerpPercent;
    private Vector3 mouseWorldPos, grapplePointPos, hookDirection;
    private Vector2 initialGrappleDirection;

    [Header("Launch Variables")]
    [SerializeField] private int launchType;
    [SerializeField] private float launchSpeed = 5f;
    private Vector2 launchMomentum;

    [Header("Hook States")]
    public bool isUnlocked = false;
    [SerializeField] private bool isLaunchToPoint = false;
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
        AimGrapple();

        OnPointGrapple();

        if (isEnemyGrappled)
        {
            if (Vector3.Distance(enemyObj.transform.position, transform.position) > 3)
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

    private void AimGrapple()
    {
        if (isUnlocked)
        {
            if (playerInput.moveAbilityInputHeld && playerInput.grappleSelected)
            {
                //Draws a line from the player towards the cursor but stops at max distance(Hook Range) to show the player how far away they can hook from
                IndicatorlerpPercent = (hookRange / Vector3.Distance(transform.position, mouseWorldPos));
                DrawLine(transform.position, Vector3.Lerp(transform.localPosition, mouseWorldPos, IndicatorlerpPercent));
                FindGrapplePoint();
            }
            else if (playerInput.moveAbilityInputRelease)
            {
                grappleLine.positionCount = 0;
            }
        }
    }

    private void FindGrapplePoint()
    {
        hookDirection = mouseWorldPos - transform.position;
        RaycastHit2D grapplehit = Physics2D.Raycast(gameObject.transform.position, hookDirection, hookRange);
        Debug.Log(grapplehit.collider);
        
        if (playerInput.meleeInput && grapplehit.collider != null && canGrapple)
        {
            if (grapplehit.collider.tag == "Enemy")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                DrawLine(transform.position, grapplePointPos);
                enemyObj = grapplehit.collider.gameObject;
                OnEnemyGrapple();
                Debug.Log("Grappled Enemy");
            }
            else if (grapplehit.collider.tag == "GrapplePoint")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                isLaunchToPoint = true;
                initialGrappleDirection = (grapplePointPos - transform.position).normalized;
                DrawLine(transform.position, grapplePointPos);
                Debug.Log("Grappled Point");
            }
            else if (grapplehit.collider.tag == null)
            {
                return;
            }
        }
    }

    
    private void OnPointGrapple()
    {
        if (isLaunchToPoint)
        {
            playerMovement.enabled = false;
            DrawLine(transform.position, grapplePointPos);
            rb2D.gravityScale = 0;
            
            if (Vector3.Distance(transform.position, grapplePointPos) < 1f)
            {
                StopGrapple();
            }
            else if (Vector3.Distance(transform.position, grapplePointPos) > 1f)
            {
                transform.position = Vector2.Lerp(transform.position, grapplePointPos, Time.deltaTime * LerpSpeed);
            }

            //Cancel Grapple
            if (playerInput.jumpKey)
            {
                StopGrapple();
                launchMomentum = initialGrappleDirection * launchSpeed;
                rb2D.velocity += launchMomentum;
            }
        }


    }

    private void OnEnemyGrapple()
    {
        playerMovement.enabled = false;
        isEnemyGrappled = true;
        corruptionScript.OnReduceCorruption(15);
        //Disable player movement
    }

    private void StopGrapple()
    {
        isLaunchToPoint = false;
        grappleLine.positionCount = 0;
        playerMovement.enabled = true;
        playerMovement.ResetAirJump();
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




