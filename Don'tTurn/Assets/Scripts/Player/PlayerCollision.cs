using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Component References
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;
    public CorruptionScript corruptionScript;
    private CheckPoint checkPointScript;
    private Knockback knockbackScript;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private Animator animator;

    public MapPanelScript mapPanelScript;

    private GameObject incomingAttacker;
    private Transform restTrans;
    private float timebetweenRegens = 1, lerpSpeed = 4f;
    public bool interactPressed = false, isInsideTrigger = false, IsMovingToTarget = false;
    private CureManager cureManager;

    private void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
        knockbackScript = GetComponent<Knockback>();
        playerMovement = GetComponent<PlayerMovement>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cureManager = GameObject.Find("CureManager").GetComponent<CureManager>();
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
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        OnBeginRest();
    }

    private void OnBeginRest()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Interact Pressed");
            interactPressed = !interactPressed;
            if (interactPressed == true)
            {
                checkPointScript.RespawnAllEnemies();
                mapPanelScript.ShowMap(checkPointScript.checkpointNumber);
                Debug.Log("Interact Pressed is true");
                playerMovement.enabled = false;
                rb2D.velocity = Vector2.zero;
                GetComponent<PlayerMovement>().playerFootsteps.stop(STOP_MODE.IMMEDIATE);
                MoveToTarget(restTrans);
            }
            else if (interactPressed == false)
            {
                Debug.Log("Interact Pressed is false");
                animator.SetBool("IsResting", false);
                DataPersistenceManager.instance?.SaveGame();
                playerMovement.enabled = true;
                IsMovingToTarget = false;
            }
        }
    }

    public void OnEnterCheckpoint(Transform restPointTrans, GameObject checkPoint) { isInsideTrigger = true; restTrans = restPointTrans; checkPointScript = checkPoint.GetComponent<CheckPoint>();}

    public void OnLeaveCheckpoint() => isInsideTrigger = false;

    IEnumerator Regenerate()
    {
        while(interactPressed && isInsideTrigger)
        {
        if (playerStats.health < playerStats.maxHealth || corruptionScript.time > 0)
        {
            playerStats.health += 20;
            corruptionScript.time -= 10;

            if (playerStats.health > playerStats.maxHealth)
            {
                playerStats.health = playerStats.maxHealth;
            }
            else if(corruptionScript.time < 0)
            {
                corruptionScript.time = 0f;
            }
        }
        yield return new WaitForSeconds(timebetweenRegens);
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
                animator.SetBool("IsResting", true);
                animator.Play("player_Rest", 0);
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

}