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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject;

            switch (attackType)
            {
                case "Eat":
                    StartCoroutine(EatEnemy());
                    break; 

            }
        }
        
    }

    public IEnumerator EatEnemy()
    {
        int random = Random.Range(0, 2); 

        

        enemy.transform.position = player.transform.position;
        enemy.GetComponent<SpriteRenderer>().enabled = false;
        enemy.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(1f); 

        if (random < 2)
        {
            Debug.Log("Will Spit out");
            enemy.GetComponent<BoxCollider2D>().enabled = true;
            enemy.GetComponent<SpriteRenderer>().enabled = true;
            enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
            yield return new WaitForSeconds(0.5f);
            enemy.GetComponent<Rigidbody2D>().velocity = transform.right * 0f;
            enemy = null; 
        }
        else if(random == 2)
        {
            Debug.Log("Killed"); 
           Object.Destroy(enemy);
            enemy = null; 
        }

        

    }
}
