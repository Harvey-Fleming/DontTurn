using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporesScript : MonoBehaviour
{

    private GameObject enemy; 
    private CorruptionScript corruptionScript;
    // Start is called before the first frame update
    void Start()
    {
        corruptionScript = GameObject.FindObjectOfType<CorruptionScript>();
        StartCoroutine(DestroyAfterTime());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                enemy = collision.gameObject; 
                Debug.Log("Hit enemy");
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(DamageAfterTime()); 
                Destroy(gameObject); 
                break;
            case "Ground":
                Destroy(gameObject);
                break; 
                

        }
    }

    public IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    public IEnumerator DamageAfterTime()
    {
        for(int i = 0; i < 5; i++)
        {
            enemy.GetComponent<EnemyStats>().OnHit(1 * corruptionScript.curseMutliplier, gameObject); 
            yield return new WaitForSeconds(1f);
        }
       
      
    }
}
