using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectiles : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private float speed;
    private Transform player;
    public PlayerStats playerStats;

    void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

    }

}