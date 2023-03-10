using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
                Debug.Log("Hit enemy");
                //do damage 
                Destroy(gameObject); 
                break;
            case "Ground":
                Destroy(gameObject);
                break; 
                

        }
    }

    public IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
