using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyKoopaMovement : MonoBehaviour
{
    public float randomNumber; 
    Rigidbody2D rb;
    public float speed;
    bool isRight; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(isRight == true)
        {
            rb.velocity = transform.right * speed;
            StartCoroutine(MovingRight()); 

        }
        else if(isRight == false)
        {
            rb.velocity = transform.right * -speed;
            StartCoroutine(MovingLeft());
        }

        StartCoroutine(RandomCheck()); 

    }

    public IEnumerator MovingRight()
    {
        yield return new WaitForSeconds(randomNumber);
        isRight = false; 
    }
    
    public IEnumerator MovingLeft()
    {
        yield return new WaitForSeconds(randomNumber);
        isRight = true; 
    }

    public IEnumerator RandomCheck()
    {
        if (randomNumber < 5)
        {
            randomNumber = Random.Range(5f, 10f); 
        }
        else
        {
            randomNumber = Random.Range(0.5f, 10f);
        }

        yield return new WaitForSeconds(randomNumber); 
       
    }





}
