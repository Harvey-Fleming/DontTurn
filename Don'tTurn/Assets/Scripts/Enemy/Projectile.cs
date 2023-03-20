using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject Target;
    [SerializeField] private float Speed;
    Rigidbody2D ProjectileRb;
    Vector2 move;
    void Start()
    {
        ProjectileRb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player");
        move = (Target.transform.position - transform.position).normalized * Speed;
        ProjectileRb.velocity = new Vector2(move.x, move.y);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
