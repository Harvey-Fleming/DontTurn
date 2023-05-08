using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
    public GameObject camHeight;
    public Camera cam;
    FollowCamera FollowCamera;
    public float camSpeed;
    // Start is called before the first frame update
    void Start()
    {
        FollowCamera = GameObject.FindGameObjectWithTag("Cam").GetComponent<FollowCamera>();
        cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<Camera>();
        camHeight = GameObject.FindWithTag("camHeight");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomOut()
    {
        cam.orthographicSize = 20;
        cam.transform.position = new Vector3(transform.position.x, 50, transform.position.z);
        Debug.Log("ZoomOut");
    }

    public void ZoomIn()
    {
        cam.orthographicSize = 6;
        FollowCamera.FixedUpdate();
        Debug.Log("zooomIn");
  
    }


} 
