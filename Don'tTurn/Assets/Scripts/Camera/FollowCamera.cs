using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
   
    public TriggerCount TriggerCount;
    public Transform playerTransform;
    [SerializeField] private float smoothTime = 0.25f, yOffset = 0f;
    private Vector3 velocity = Vector3.zero;
    public bool followPlayer = true;
    Vector3 newPos;
    public GrappleAbility grapple;

    private void Awake()
    {
        TriggerCount = GameObject.FindWithTag("Cam").GetComponent<TriggerCount>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void FixedUpdate()
    {
        if (followPlayer)
        {
            newPos = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, -10f);
        }
        else
        {
            newPos = grapple.camPoint;
        }
        
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(newPos.x, newPos.y, -10f), ref velocity, smoothTime);

    }

    

    void OnSceneLoaded()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    
}
