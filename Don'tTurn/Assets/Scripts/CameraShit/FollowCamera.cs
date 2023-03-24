using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    private int divNumber;
    public TriggerCount TriggerCount;
    public Transform playerTransform;
    [SerializeField] private float smoothTime = 0.25f, yOffset = 0f;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        divNumber = 2;
        TriggerCount = GameObject.FindWithTag("Cam").GetComponent<TriggerCount>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

    }

    internal void ZoomIn(GameObject gameObject)
    {
        throw new NotImplementedException();
    }

    void OnSceneLoaded()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    public void ZoomIn()
    {
        GetComponent<Camera>().fieldOfView--;
    }


    public void ZoomOut()
    {
        GetComponent<Camera>().fieldOfView++;
    }
}
