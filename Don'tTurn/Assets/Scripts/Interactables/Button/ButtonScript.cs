using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    bool isActive;
    int pressedTimes = 0; 
    Animator animator; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isActive == true && pressedTimes == 0)
        {

            StartCoroutine(Pressed()); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
            Debug.Log("Press E!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false; 
    }


    public IEnumerator Pressed()
    {
        pressedTimes = 1;
        animator.SetBool("isActive", true); 
        Debug.Log("It has been pressed");
        Debug.Log("Doing the Pressed thing");
        yield return new WaitForSeconds(3f);
        Debug.Log("Undoing the stuff");
        Debug.Log("It is being unpressed");
        animator.SetBool("isActive", false);
        pressedTimes = 0;

    }
}
