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
    public CureChamberCode chamber;
    bool canShake = false;
    Vector3 originalPos;
    public float shakeAmount;

    private void Awake()
    {
        TriggerCount = GameObject.FindWithTag("Cam").GetComponent<TriggerCount>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void FixedUpdate()
    {
        if (!chamber.zoomOnChamber)
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
        else
        {
            if (!canShake)
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(newPos.x, newPos.y, -10f), ref velocity, smoothTime);
            }
            else
            {
                transform.position = new Vector3(originalPos.x + UnityEngine.Random.Range(-0.1f * shakeAmount, 0.1f * shakeAmount), originalPos.y + UnityEngine.Random.Range(-0.1f * shakeAmount, 0.1f * shakeAmount), -10);
            }

            newPos = chamber.transform.position;
            Camera cam = GetComponent<Camera>();
            if (cam.orthographicSize > 4f)
            {
                cam.orthographicSize -= 0.3f * Time.deltaTime;
            }
        }
    }

    public void Shake()
    {
        StartCoroutine(StartShake());
    }

    

    void OnSceneLoaded()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    IEnumerator StartShake()
    {
        yield return new WaitForSeconds(3f);
        originalPos = transform.position;
        chamber.startLights = true;
        canShake = true;
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("WinScreen");
    }
    
    
}
