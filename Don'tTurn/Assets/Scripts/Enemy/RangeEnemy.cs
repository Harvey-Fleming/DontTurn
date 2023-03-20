using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    public float timeBtwShots;
    [SerializeField] private float startTimeBtwShots = 1f;
    [SerializeField] private bool InSite;
    public Transform player;
    public GameObject projectile;
    public float projectileSpeed;
    [SerializeField] private float lineOfSite;
    private Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        InSite = false;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnDrawGizmosSelected() 
    {
            print("on draw gizmos");
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    // Update is called once per frame
    void Update()
    {

        // set float (distanceFromPlayer) as distance between the player and enemy
        // if the distance between the enemy and player is less than the lineofsite spawn projectile
        //Time.time is the almount of secound since start of the game
        // and timeBtwShot less than start time than spawn projectile (this will aslo add delay from initial start time
        // timebtwshots should fire every secound
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        Debug.Log("DistanceFromPlayer" + distanceFromPlayer.ToString());
        if (distanceFromPlayer < lineOfSite && timeBtwShots < Time.time)
        {
            InSite = true;
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = Time.time + startTimeBtwShots;
            anim.SetBool("Awaken", true);
        }

       // else if (distanceFromPlayer > lineOfSite && timeBtwShots < Time.time)
      //  {
         //   InSite = false;
         //  anim.SetBool("Awaken", false);
       // }

    }
}
