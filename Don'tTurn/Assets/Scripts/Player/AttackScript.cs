using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    [Header("Melee Attack Stats")]

    [SerializeField] private Transform attackCirclePoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldownTime = 1.0f;
    [SerializeField] private bool canAttack = true;

    [Header("Corruption Effect")]
    //[SerializeField] private CorruptionScript corruptionScript;
    private float attackSpeedMultiplier;

    // Update is called once per frame
    void Update()
    {
        MeleeAttackCooldown();
    }

    private void MeleeAttackCooldown()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attackSpeedMultiplier = (1);
            StartCoroutine(AttackCooldown());
        }

    }

    void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackCirclePoint.position, attackRadius);

        foreach(Collider2D Enemy in enemiesHit)
        {
            Enemy.GetComponent<EnemyStats>()?.OnHit(attackDamage);
        } 
        Debug.Log("Attacked");
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(attackCirclePoint.position, attackRadius);
    }

    IEnumerator AttackCooldown()
    {
        while (canAttack == true)
        {
            MeleeAttack();
            canAttack = false;
            yield return new WaitForSeconds(attackCooldownTime * attackSpeedMultiplier);
            canAttack = true;   
            yield break;
        }
    
    }
}
