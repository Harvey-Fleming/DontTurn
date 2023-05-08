using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCount : MonoBehaviour
{
    CamZoom Cam;
    FollowCamera FollowCamera;
    public static int countDown;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<CamZoom>();
        FollowCamera = GameObject.FindGameObjectWithTag("Cam").GetComponent<FollowCamera>();
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
            Cam.ZoomOut();
        }

        else if (countDown == 0)
        {
            Cam.ZoomIn();

        }

        
    }
    
    // Update is called once per frame
    public void Update()
    {
     
    }
}
