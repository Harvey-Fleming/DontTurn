using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueParentScript : MonoBehaviour
{
    public Button[] buttons; 
    public bool isPressed; //checks if a button is pressed and turns off both of the child objects 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
               buttons[i].gameObject.SetActive(false); 
            }

            isPressed = false; 
           
        }
    }
}
