using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    Animator animator;
    public bool isActive;
    int State = 0; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isActive == true)
        {
            switch (State)
            {
                case 0:
                    Pressed();
                    break;
                case 1:
                    UnPressed();
                    break; 
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
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
        Debug.Log("It has been pressed, do the thing");
        State = 1;
        animator.SetBool("isActive", true); 
    }

    void UnPressed()
    {
        Debug.Log("It has been UNPREssed, do the thing");
        State = 0;
        animator.SetBool("isActive", false);
    }
}
