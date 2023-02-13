using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCamera : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, newPos,ref velocity, smoothTime);
    }

    void OnSceneLoaded()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
}
