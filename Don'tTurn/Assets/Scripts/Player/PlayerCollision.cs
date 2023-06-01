using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Script References
    private PlayerStats playerStats;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    public CorruptionScript corruptionScript;
    private CheckPoint checkPointScript;
    private Knockback knockbackScript;
    private GrappleAbility grappleAbility;
    [SerializeField] private PrototypeDash DashAbility;
    private CurseAttacks CursePunch;    
    public MapPanelScript mapPanelScript;
    private CureManager cureManager;

    //Component References
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private Animator animator;

    private GameObject incomingAttacker;
    [SerializeField] private Transform restTrans;
    [SerializeField] private Transform tempRestTrans;
    [SerializeField] private float timeBetweenRegens = 0.5f, lerpSpeed = 4f;
    public bool interactPressed = false, isInsideTrigger = false, IsMovingToTarget = false, canStandUp = true, isResting =false;
 

    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
        playerInput = GetComponent<PlayerInput>();
        knockbackScript = GetComponent<Knockback>();
        playerMovement = GetComponent<PlayerMovement>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cureManager = GameObject.Find("CureManager").GetComponent<CureManager>();
        grappleAbility = GetComponent<GrappleAbility>();
        DashAbility = GetComponent<PrototypeDash>();
        CursePunch = GetComponent<CurseAttacks>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GrapplePoint"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cure")
        {
            cureManager.IncreaseCureCount();
            collision.gameObject.GetComponent<CureID>().hasCollectedCure = true;
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        OnBeginRest();
    }

    private void OnBeginRest()
    {
        if (isInsideTrigger && playerInput.interactInput && canStandUp)
        {
            interactPressed = !interactPressed;
            if (interactPressed == true)
            {
                restTrans = tempRestTrans;
                checkPointScript.RespawnAllEnemies();
                mapPanelScript.ShowMap(checkPointScript.checkpointNumber);
                checkPointScript.hasVisited = true;
                isResting = true;
                grappleAbility.StopGrapple();
                DisableAbilities();
                rb2D.velocity = Vector2.zero;
                GetComponent<PlayerMovement>().playerFootsteps.stop(STOP_MODE.IMMEDIATE);
                MoveToTarget(restTrans);
            }
            else if (interactPressed == false)
            {
                EnableAbilities();
                isResting = false;
                animator.SetBool("IsResting", false);
                DataPersistenceManager.instance?.SaveGame();
                IsMovingToTarget = false;
            }
        }
    }

    public void OnEnterCheckpoint(Transform restPointTrans, GameObject checkPoint) { isInsideTrigger = true; tempRestTrans = restPointTrans; checkPointScript = checkPoint.GetComponent<CheckPoint>();}

    public void OnLeaveCheckpoint() {isInsideTrigger = false; EnableAbilities(); animator.SetBool("IsResting", false); interactPressed = false; DashAbility.shotgunUI.SetActive(true);}

    IEnumerator Regenerate()
    {
        while(interactPressed && isInsideTrigger)
        {
            animator.SetBool("IsResting", true);
            DashAbility.shotgunUI.SetActive(false);
            if (playerStats.health < playerStats.maxHealth || corruptionScript.time > 0)
            {
                playerStats.health += 20;
                corruptionScript.time -= 10;
                animator.Play("player_Rest");
            }

            //Keep the player health at max and do not heal over it
            if (playerStats.health > playerStats.maxHealth)
            {
                 playerStats.health = playerStats.maxHealth;
            }
            else if(corruptionScript.time < 0)
            {
                corruptionScript.time = 0f;
            }  
        yield return new WaitForSeconds(timeBetweenRegens);
        }
    }

    private void FixedUpdate() 
    {
        if(IsMovingToTarget)
        {
            if(Mathf.Abs(transform.position.x - restTrans.position.x) > 0.01f)
            {
                animator.SetFloat("Speed", 1);
                transform.position = Vector2.Lerp(transform.position, new Vector2(restTrans.position.x, transform.position.y), Time.deltaTime * lerpSpeed);
            }
            else if (Mathf.Abs(transform.position.x - restTrans.position.x) < 0.05f)
            {
                transform.position = restTrans.position;
                IsMovingToTarget = false;
                StartCoroutine("Regenerate");
            }
        }
    }


    private void MoveToTarget(Transform targetTrans)
    {
        restTrans = targetTrans;
        IsMovingToTarget = !IsMovingToTarget;
    }

    private void DisableAbilities()
    {
        playerMovement.enabled = false;
        grappleAbility.enabled = false;
        DashAbility.enabled = false;
        CursePunch.enabled = false;
    }

    private void EnableAbilities()
    {
        playerMovement.enabled = true;
        grappleAbility.enabled = true;
        DashAbility.enabled = true;
        CursePunch.enabled = true;
    }

}