using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform player;
    [SerializeField] private float lineOfSite;
    [SerializeField] private float ShootingRange;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject projectileParent;
    [SerializeField] private float FireRate = 1f;
    [SerializeField] private float nextFireTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= ShootingRange && nextFireTime <Time.time)
        {
            Instantiate(Projectile, projectileParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + FireRate;
        }
        
    {
        
    }
}
}
