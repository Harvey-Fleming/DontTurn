using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class GrappleAbility : MonoBehaviour, IDataPersistence
{
    //Component / Object references
    private GameObject enemyObj;
    private SpringJoint2D joint2D;
    private LineRenderer grappleLine;
    private Rigidbody2D rb2D;

    //Script References
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private Cursor cursorScript;
    private CorruptionScript corruptionScript;
    private FallDamage fallDamage;
    public FollowCamera followCam;
    public Vector2 camPoint;

    [Header("Hook Variables")]
    [SerializeField] private float hookRange = 5f;
    [SerializeField] private float enemySlerpSpeed = 4f, LerpSpeed = 8f, launchSpeed = 5f;
    [SerializeField] private float grappleCooldown = 1f;
    private float hookAngle, IndicatorlerpPercent;
    private Vector3 mouseWorldPos, grapplePointPos, hookDirection;
    private Vector2 initialGrappleDirection;
    public float hookDistance;
    public Vector2 endPoint;

    [Header("Hanging Variables")]
    private bool isHanging;
    private float jointdampingRatio, jointfrequency;

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
        fallDamage = GetComponent<FallDamage>();
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
                followCam.followPlayer = false;
                IndicatorlerpPercent = (hookRange / Vector3.Distance(transform.position, mouseWorldPos));
                endPoint = Vector3.Lerp(transform.localPosition, mouseWorldPos, IndicatorlerpPercent);
                camPoint = Vector3.Lerp(transform.localPosition, endPoint, 0.5f);
                DrawLine(transform.position, endPoint);
                FindGrapplePoint();
            }
            else if (playerInput.moveAbilityInputRelease)
            {
                grappleLine.positionCount = 0;
                followCam.followPlayer = true;
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
                isGrappling = true;
                initialGrappleDirection = (transform.position - grapplePointPos).normalized;
                DrawLine(transform.position, grapplePointPos);
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

    
    private void OnPointGrapple()
    {
        if (isGrappling)
        {
            playerMovement.enabled = false;
            DrawLine(transform.position, grapplePointPos);
            rb2D.gravityScale = 0;
            
            if (Vector3.Distance(transform.position, grapplePointPos) < hookDistance)
            {
                StopGrapple();
                rb2D.velocity = -initialGrappleDirection * launchSpeed;
            }
            else if (Vector3.Distance(transform.position, grapplePointPos) > hookDistance)
            {
                transform.position = Vector2.Lerp(transform.position, grapplePointPos, Time.deltaTime * LerpSpeed);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            //Cancel Grapple
            if (playerInput.jumpKey)
            {
                StopGrapple();
            }
        }
    }

    private void HangOn()
    {
        joint2D = gameObject.AddComponent<SpringJoint2D>();

        joint2D.autoConfigureDistance = false;
        joint2D.autoConfigureConnectedAnchor = false;

        joint2D.connectedAnchor = grapplePointPos;

        joint2D.distance = Vector2.Distance(transform.position, grapplePointPos);

        joint2D.dampingRatio = jointdampingRatio;
        joint2D.frequency = jointfrequency;

    }

    private void OnEnemyGrapple()
    {
        playerMovement.enabled = false;
        isEnemyGrappled = true;
        corruptionScript.OnReduceCorruption(15);
        //Disable player movement
    }

    public void StopGrapple()
    {
        fallDamage.maxYVel = 0;
        playerMovement.ResetAirJump();
        isGrappling = false;
        grappleLine.positionCount = 0;
        playerMovement.enabled = true;
        StartCoroutine("Cooldown");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

    //Save and Loading Data
    public void LoadData(GameData data)
    {
        this.isUnlocked = data.isGrappleUnlocked;
    }

    public void SaveData(GameData data)
    {
        data.isGrappleUnlocked = this.isUnlocked;
    }

}




