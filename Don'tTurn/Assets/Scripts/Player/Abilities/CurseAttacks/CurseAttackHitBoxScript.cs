using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAttackHitBoxScript : MonoBehaviour
{
    [SerializeField] 
    public string attackType;
    //2 attack types "Eat" and "Punch"
   [SerializeField] GameObject enemy;
    public GameObject player;
    bool isColliding = false; 
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
        if(collision.gameObject.CompareTag("Enemy") && isColliding == false)
        {
            isColliding = true; 
            enemy = collision.gameObject;

            switch (attackType)
            {
                case "Eat":
                    Debug.Log("Entering Coroutine"); 
                    StartCoroutine(EatEnemy());
                    break;
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

    public IEnumerator EatEnemy()
    {
        int random = 0; 
        Debug.Log("In coroutine" + random); 
        if(isColliding == true)
        {
            enemy.transform.position = player.transform.position;
            enemy.transform.position += new Vector3(2f, 0f, 0f);
            enemy.GetComponent<SpriteRenderer>().enabled = false;
            enemy.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(0.2f);

            if (random == 0)
            {
                Debug.Log("Will Spit out");
                enemy.GetComponent<BoxCollider2D>().enabled = true;
                enemy.GetComponent<SpriteRenderer>().enabled = true;
                //enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
                yield return new WaitForSeconds(0.5f);
                //enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 0f;
                enemy = null;
            }
            else if (random == 2)
            {
                Debug.Log("Killed");
                Object.Destroy(enemy);
                enemy = null;
            }

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
        enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
        gameObject.SetActive(false); 
    }
}