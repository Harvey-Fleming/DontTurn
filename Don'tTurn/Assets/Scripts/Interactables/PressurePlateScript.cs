using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    Animator animator;
    bool isActive = false;
    int timesPressed = 0; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && timesPressed == 0)
        {
            Pressed(); 
        }
        else if (!isActive)
        {
            UnPressed(); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false; 
    }

    void Pressed()
    {
        timesPressed = 1;
        animator.SetBool("isActive", true);
        Debug.Log("DO THE THING!!"); 
    }

    void UnPressed()
    {
        timesPressed = 0;
        animator.SetBool("isActive", false);
        Debug.Log("DO THE THING!!");
    }
}
