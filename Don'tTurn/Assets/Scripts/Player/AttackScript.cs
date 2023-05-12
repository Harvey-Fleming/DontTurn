using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class AttackScript : MonoBehaviour
{
    //Component References
    private CorruptionScript corruptionScript;
    private Animator animator;
    private PlayerInput playerInput;
    private PauseMenu pauseMenu;
    private GrappleAbility grappleAbility;

    [Header("Melee Attack Stats")]
    [SerializeField] private Transform attackPointTrans;
    [SerializeField] private float attackRadius, attackCooldownTime = 0.5f, baseAttackDamage, UpgradeDamage;
    public bool canAttack = true;
    public GameObject corruptionImage;

    [Header("Melee Attack Combo Variables")]
    [SerializeField] private int currentAttackNumber;
    [SerializeField] private float comboTimer, maxcomboTimer = 0.4f;
    [SerializeField] private bool inComboWindow;

    private void Start() 
    {
        playerInput = GetComponent<PlayerInput>();
        corruptionScript = FindObjectOfType<CorruptionScript>();
        animator = GetComponent<Animator>();
        pauseMenu = GetComponent<PauseMenu>();
        grappleAbility = GetComponent<GrappleAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleComboTimer();

        //Trigger first attack in the combo
        if (playerInput.meleeInput && canAttack && comboTimer <= 0 && Time.timeScale != 0)
        {
            currentAttackNumber = 1;
            animator.SetBool("IsAttacking", true);
            if (grappleAbility.aimingGrapple == false && GetComponent<PlayerCollision>().isResting == false)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Melee1, this.transform.position);
            }
        }
    }

    void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPointTrans.position, attackRadius);

        foreach(Collider2D Enemy in enemiesHit)
        {
            Enemy.GetComponent<EnemyStats>()?.OnHit(baseAttackDamage + UpgradeDamage, this.gameObject);
        } 

        comboTimer = maxcomboTimer;
        inComboWindow = true;

        //starts combo attack timer
        //if attack input during timer, next attack plays.
    }

    private void HandleComboTimer()
    {
        if(inComboWindow == true)
        {
            if (comboTimer >= 0)
            {
                comboTimer -= 1 * Time.deltaTime;

                if(playerInput.meleeInput)
                {

                    if (currentAttackNumber < 3)
                    {
                        currentAttackNumber ++;
                        inComboWindow = false;
                        animator.SetInteger("AttackNumber", currentAttackNumber);
                    }
                    else if (currentAttackNumber ==  3)
                    {
                        comboTimer = 0;
                        StartCoroutine(MeleeCooldown());
                    }

                    if (currentAttackNumber == 2 && grappleAbility.aimingGrapple == false)
                    {
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.Melee2, this.transform.position);
                    }

                    if (currentAttackNumber == 3 && comboTimer != 0 && grappleAbility.aimingGrapple == false)
                    {
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.Melee3, this.transform.position);
                    }
                }
            }
            else if (comboTimer <= 0)
            {
                currentAttackNumber = 1;
                animator.SetInteger("AttackNumber", currentAttackNumber);
                animator.SetBool("IsAttacking", false);
                StartCoroutine(MeleeCooldown());
                inComboWindow = false;
            }
        }
    }

    private void AttackAnimationEvent()
    {
        canAttack = false;
        MeleeAttack();
    }

    IEnumerator MeleeCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;   
        yield break;
    }

    public void OnMeleeUpgrade()
    {
        UpgradeDamage += 1;
    }

    public void ResetWindow()
    {
        comboTimer = 0;
        inComboWindow = true;
        currentAttackNumber = 1;
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(attackPointTrans.position, attackRadius);
    }


}
