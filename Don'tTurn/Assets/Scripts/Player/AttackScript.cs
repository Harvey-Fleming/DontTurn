using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    [Header("Melee Attack Stats")]

    [SerializeField] private Transform attackCirclePoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;
    [SerializeField]private float attackCooldownTime = 1f;
        
    private bool isTimerDone = true;
    private float timeToNextAttack;

    [Header("Corruption Effect")]
    [SerializeField] private CorruptionScript corruptionScript;
    private int attackSpeedMultiplier;



    
    // Start is called before the first frame update
    void Start()
    {
        timeToNextAttack = attackCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAttackCooldown();
    }

    private void MeleeAttackCooldown()
    {
        if (isTimerDone)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                MeleeAttack();
                isTimerDone = false;
            }
        }
        else
        {
            timeToNextAttack -= 1 * Time.deltaTime;
            if (timeToNextAttack <= 0)
            {
                isTimerDone = true;
                timeToNextAttack = attackCooldownTime;
                return;
            }
        }
    }

    void MeleeAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackCirclePoint.position, attackRadius);

        foreach (Collider2D Enemy in enemiesHit)
        {
            Enemy.GetComponent<EnemyStats>().OnHit(attackDamage);
        } 
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(attackCirclePoint.position, attackRadius);
    }
}
