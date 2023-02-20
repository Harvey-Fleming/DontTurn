using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject[] spores; 
    bool isCollidingTrue; 
    Transform enemy; 
    public float speed = 20f;
    public Rigidbody2D rb; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
      
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollidingTrue == true)
        {
            transform.position = enemy.position;
        }
    }

    public IEnumerator Move()
    {
        rb.velocity = transform.right * speed;

        yield return new WaitForSeconds(1.5f);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;


        for (int i = 0; i < spores.Length; i++)
        {
            GameObject spore = spores[i];
            spore.gameObject.SetActive(true); 
        }
        
        gameObject.SetActive(false); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.transform;
            isCollidingTrue = true;

        }
    }
}
