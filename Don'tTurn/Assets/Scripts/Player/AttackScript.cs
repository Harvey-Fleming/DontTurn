using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private CorruptionScript corruptionScript;
    [SerializeField] private Animator animator;

    [Header("Melee Attack Stats")]

    [SerializeField] private Transform attackPointTrans;
    [SerializeField] private float attackRadius, attackCooldownTime = 0.5f;
    [SerializeField] private int baseAttackDamage, UpgradeDamage;
    private bool canAttack = true;


    private void Start() 
    {
        corruptionScript = FindObjectOfType<CorruptionScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(AttackCooldown());
        }
    }

    void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPointTrans.position, attackRadius);

        foreach(Collider2D Enemy in enemiesHit)
        {
            Enemy.GetComponent<EnemyStats>()?.OnHit(baseAttackDamage + UpgradeDamage, this.gameObject);
        } 
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(attackPointTrans.position, attackRadius);
    }

    IEnumerator AttackCooldown()
    {
        while (canAttack == true)
        {
            
            MeleeAttack();
            animator.SetBool("IsAttacking", true);
            yield return 0;
            animator.SetBool("IsAttacking", false);
            canAttack = false;
            yield return new WaitForSeconds(attackCooldownTime);
            canAttack = true;   
            yield break;
        }
    
    }

    public void OnMeleeUpgrade()
    {
        UpgradeDamage += 5;
    }


}
