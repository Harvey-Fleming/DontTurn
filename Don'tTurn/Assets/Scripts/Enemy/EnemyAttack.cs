using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private Transform attackPointTrans;
    [SerializeField] private float attackDamage, attackRadius;


    // Start is called before the first frame update
    void Start()
    {
        attackPointTrans = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MeleeAttack()
    {
        Collider2D[] Hit = Physics2D.OverlapCircleAll(attackPointTrans.position, attackRadius);

        foreach(Collider2D Collider in Hit)
        {
            Collider.GetComponent<PlayerStats>()?.OnHit(attackDamage, this.gameObject);
        } 
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(attackPointTrans.position, attackRadius);
    }


}

