using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CorruptionScript corruptionScript;
    [SerializeField] private Animator animator;

    [Header("Melee Attack Stats")]

    [SerializeField] private Transform attackPointTrans;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldownTime = 2.0f;
    [SerializeField] private bool canAttack = true;

    [Header("Corruption Effect")]
    private float attackSpeedMultiplier;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            attackSpeedMultiplier = (1 + (corruptionScript.time / 100));
            StartCoroutine(AttackCooldown());
        }
    }

    void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPointTrans.position, attackRadius);

        foreach(Collider2D Enemy in enemiesHit)
        {
            Enemy.GetComponent<EnemyStats>()?.OnHit(attackDamage);
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
            canAttack = false;
            yield return new WaitForSeconds(attackCooldownTime / attackSpeedMultiplier);
            animator.SetBool("IsAttacking", false);
            canAttack = true;   
            yield break;
        }
    
    }
}
