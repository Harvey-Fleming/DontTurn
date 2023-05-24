using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUIBob : MonoBehaviour
{
    float startY;
    float y;
    public float bobSpeed;
    public float bobAmount;
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        y = startY + (Mathf.Sin(Time.time * bobSpeed) * bobAmount / 1000f);

        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
