using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCount : MonoBehaviour
{
    CamZoom Cam;
    public static int countDown;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<CamZoom>();

        //static  CallCheck();
    }


    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CamTrigger")
        {
            countDown++;

            Debug.Log(countDown);

            countDown = countDown % 2;

            Debug.Log(countDown);

            CallCheck();
        }

    }
    

     void CallCheck()
    {
        if (countDown != 0)
        {
            Cam.ZoomIn();
          

        }

        else if (countDown == 0)
        {
            Cam.ZoomOut();
           
        }

        
    }
    
    // Update is called once per frame
    public void Update()
    {
     
    }
}
