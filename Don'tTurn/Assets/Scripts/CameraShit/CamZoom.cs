using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
    public Camera cam;
    public float camSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<Camera>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn()
    {
        cam.orthographicSize = 4;
        Debug.Log("zooomIn");
  
    }


    public void ZoomOut()
    {
        cam.orthographicSize = 6;
        Debug.Log("ZoomOut");
    }
} 
