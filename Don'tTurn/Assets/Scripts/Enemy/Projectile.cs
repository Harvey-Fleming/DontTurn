using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public PlayerStats playerStats;
    GameObject Target;
    [SerializeField] private float Speed;
    Rigidbody2D ProjectileRb;
    Vector2 move;
    void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void Start()
    {
        ProjectileRb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player");
        Vector2 move = (Target.transform.position - transform.position).normalized * Speed;
        ProjectileRb.velocity = new Vector2(move.x, move.y);
        Destroy(gameObject, 5f);


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.health -= 10f;
            Destroy(gameObject);
            print("Collision made");
        }
    }
}
