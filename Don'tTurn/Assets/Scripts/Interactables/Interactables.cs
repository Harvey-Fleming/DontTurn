using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    private Animator animator;
    public bool isActive;
    public bool isPressedOnce; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.W) && isActive == true)
        {
            if(isPressedOnce == false)
            {
                Pressed();
            }
            else if(isPressedOnce == true)
            {
                UnPressed(); 
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isActive = true;
            Debug.Log("PRESS E!"); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
        animator.SetBool("isActive", false); 
    }

    public void Pressed()
    {
        animator.SetBool("isActive", true);
        isPressedOnce = true;
    }

    public void UnPressed()
    {
        animator.SetBool("isActive", false);
        isPressedOnce = false; 
    }
}
