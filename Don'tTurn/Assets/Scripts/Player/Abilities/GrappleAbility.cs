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
    private Animator animator;

    //Script References
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private CursorScript cursorScript;
    private AttackScript attackScript;
    private FallDamage fallDamage;
    public FollowCamera followCam;
    public Vector2 camPoint;
    private Transform grappleLineSpawn;

    public LayerMask layerMask;

    [Header("Hook Variables")]
    [SerializeField] private float hookRange = 5f;
    [SerializeField] private float enemySlerpSpeed = 4f, LerpSpeed = 8f, launchSpeed = 5f;
    [SerializeField] private float grappleCooldown = 1f;
    private float hookAngle, IndicatorlerpPercent, gravityScale = 4f;
    private Vector3 mouseWorldPos, grapplePointPos, hookDirection;
    private Vector2 initialGrappleDirection;
    public float hookDistance;
    public Vector2 endPoint;
    [SerializeField] private Material aimMaterial;
    [SerializeField] private Material grappleMaterial;

    [Header("Hook States")]
    public bool isUnlocked = false;
    [SerializeField] private bool isGrappling = false;
    [SerializeField] private bool isEnemyGrappled = false;
    [SerializeField] private bool canGrapple = true;
    public bool aimingGrapple = false;

    private void Awake() 
    {
        cursorScript = GameObject.Find("Cursor").GetComponent<CursorScript>();
        attackScript = GetComponent<AttackScript>();
        grappleLine = GetComponent<LineRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
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
                aimingGrapple = true;
                attackScript.ResetWindow();
                followCam.followPlayer = false;
                animator.SetBool("IsAttacking", false);
                IndicatorlerpPercent = (hookRange / Vector3.Distance(transform.position, mouseWorldPos));
                endPoint = Vector3.Lerp(transform.localPosition, mouseWorldPos, IndicatorlerpPercent);
                camPoint = Vector3.Lerp(transform.localPosition, endPoint, 0.5f);
                DrawLine(transform.position, endPoint);
                grappleLine.material = aimMaterial;
                FindGrapplePoint();
            }
            else if (playerInput.moveAbilityInputRelease)
            {
                attackScript.ResetWindow();
                grappleLine.positionCount = 0;
                followCam.followPlayer = true;
                aimingGrapple = false;
            }
        }
    }

    private void FindGrapplePoint()
    {
        hookDirection = mouseWorldPos - transform.position;
        RaycastHit2D grapplehit = Physics2D.Raycast(gameObject.transform.position, hookDirection, hookRange, layerMask);
        
        if (playerInput.meleeInput && grapplehit.collider != null && canGrapple)
        {
            if (grapplehit.collider.tag == "Enemy")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                grappleLine.material = grappleMaterial;
                DrawLine(transform.position, grapplePointPos);
                enemyObj = grapplehit.collider.gameObject;
                OnEnemyGrapple();
                AudioManager.instance.PlayOneShot(FMODEvents.instance.grappleHook, this.transform.position);
            }
            else if (grapplehit.collider.tag == "GrapplePoint")
            {
                grapplePointPos = grapplehit.collider.transform.position;
                isGrappling = true;
                initialGrappleDirection = (transform.position - grapplePointPos).normalized;
                DrawLine(transform.position, grapplePointPos);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.grappleHook, this.transform.position);
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
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsGrappling", true);
            playerMovement.enabled = false;
            grappleLineSpawn = transform.GetChild(6);
            grappleLine.material = grappleMaterial;
            DrawLine(grappleLineSpawn.position, grapplePointPos);
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
            //if (playerInput.jumpKey)
            //{
            //    StopGrapple();
            //}
        }
    }

    private void OnEnemyGrapple()
    {
        animator.SetBool("IsGrappling", true);
        playerMovement.enabled = false;
        isEnemyGrappled = true;
        //Disable player movement
    }

    public void StopGrapple()
    {
        fallDamage.maxYVel = 0;
        isGrappling = false;
        animator.SetBool("IsGrappling", false);
        playerMovement.ResetAirJump();
        rb2D.gravityScale = gravityScale;
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




