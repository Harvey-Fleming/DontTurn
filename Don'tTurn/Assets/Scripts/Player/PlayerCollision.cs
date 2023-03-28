using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Component References
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;
    private CorruptionScript corruptionScript;
    private CheckPoint checkPointScript;
    private Knockback knockbackScript;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private Animator animator;

    private GameObject incomingAttacker;
    private Transform restTrans;
    private int attackDamage = 10;
    private float timebetweenRegens = 1, iframeflicker = 0.1f, lerpSpeed = 4f;
    private bool canTakeDamage = true, interactPressed = false;
    public bool isInsideTrigger = false, IsMovingToTarget = false;

     private void Start() 
     {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
        corruptionScript = GameObject.FindObjectOfType<CorruptionScript>();
        knockbackScript = GetComponent<Knockback>();
        playerMovement = GetComponent<PlayerMovement>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
     }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            playerStats.OnHit(attackDamage, collision.gameObject);
            corruptionScript.OnHitCorruption(attackDamage);
            knockbackScript.ApplyKnockBack(collision.gameObject);
            StartCoroutine("IFrames");
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
                Debug.Log("Interact Pressed is true");
                playerMovement.enabled = false;
                rb2D.velocity = Vector2.zero;
                MoveToTarget(restTrans);
                StartCoroutine("Regenerate");
            }
            else if (interactPressed == false)
            {
                Debug.Log("Interact Pressed is false");
                animator.SetBool("IsResting", false);

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

    IEnumerator IFrames()
    {
        canTakeDamage = false;
        for (int i = 3; i > 0; i--)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(iframeflicker);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(iframeflicker);
        }
        canTakeDamage = true;
        yield break;
    }

    private void FixedUpdate() {
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
            }
        }
    }

    private void MoveToTarget(Transform targetTrans)
    {
        restTrans = targetTrans;
        IsMovingToTarget = !IsMovingToTarget;
    }

}