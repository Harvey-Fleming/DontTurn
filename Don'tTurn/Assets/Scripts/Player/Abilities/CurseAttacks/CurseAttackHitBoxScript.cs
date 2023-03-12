using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAttackHitBoxScript : MonoBehaviour
{
    [SerializeField] 
    public string attackType;
    //2 attack types "Eat" and "Punch"
   [SerializeField] GameObject enemy;
    [SerializeField] Rigidbody2D enemyRigidBody; 
    public GameObject player;
    [SerializeField] bool isColliding = false; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isColliding == false)
        {
            StartCoroutine(NoCollision()); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isColliding == false)
        {
            isColliding = true;
            enemy = collision.gameObject;

            switch (attackType)
            {
                //case "Eat":
                //    Debug.Log("Entering Coroutine");
                //    StartCoroutine(EatEnemy());
                //    break;
                case "Punch":
                    PunchEnemy();
                    break;
            }
        }
        else
        {
            isColliding = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isColliding == false)
        {
            isColliding = true;
            enemy = collision.gameObject;
            enemyRigidBody = enemy.GetComponent<Rigidbody2D>();

            switch (attackType)
            {
                case "Eat":
                    Debug.Log("Entering Coroutine");
                    StartCoroutine(EatEnemy());
                    break;
                case "Punch":
                    Debug.Log("Entering Coroutine for Punch"); 
                    PunchEnemy();
                    break;
            }
        }
        else
        {
            isColliding = false;
        }
    }

    public IEnumerator EatEnemy()
    {
        Debug.Log("In coroutine"); 
        if(isColliding == true)
        {
            enemy.transform.position = player.transform.position;
            enemy.transform.position += new Vector3(1f, 0f, 0f);
            enemy.GetComponent<SpriteRenderer>().enabled = false;
            enemy.GetComponent<BoxCollider2D>().enabled = false;
            enemyRigidBody = enemy.GetComponent<Rigidbody2D>(); 
            enemyRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            Debug.Log("Waiting 0.1 second");
            yield return new WaitForSeconds(0.1f);
            enemy.GetComponent<EnemyStats>().OnHit(20, player); 
            if(enemy.GetComponent<EnemyStats>().isDead == true)
            {
               
                player.GetComponent<CorruptionScript>().time -= 20f;
                Debug.Log("Healed!");
                enemy.GetComponent<BoxCollider2D>().enabled = true;
                enemyRigidBody.constraints = RigidbodyConstraints2D.None;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                enemy.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Debug.Log("Time has gone");
                Debug.Log("Will Spit out");
                enemy.GetComponent<BoxCollider2D>().enabled = true;
                enemyRigidBody.constraints = RigidbodyConstraints2D.None;
                enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                enemy.GetComponent<SpriteRenderer>().enabled = true;
                //enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
                enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
                //Enemy on hit Function :D
                //enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 0f;
                enemy = null;
            }
           

            enemy = null;
            gameObject.SetActive(false);
            isColliding = false; 
        }

        
        

    }

    public IEnumerator NoCollision()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false); 
    }

   public void PunchEnemy()
    {
        if(player.GetComponent<CorruptionScript>().time >= 10)
        {
            player.GetComponent<CorruptionScript>().time += 10f;
        }
        else
        {
            player.GetComponent<PlayerStats>().OnHit(10, gameObject); 
        }
       
        enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
        //gameObject.SetActive(false);
        isColliding = false;
        enemy = null; 
    }
}
