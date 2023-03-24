using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCount : MonoBehaviour
{
    FollowCamera Cam;
    public int countDown;
 
    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<FollowCamera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            countDown++;

            Debug.Log(countDown);

            _ = countDown % 2;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (countDown % 2 == 0)
        {
            Cam.ZoomIn();
        }

        else if (countDown % 2 != 0)
        {
            Cam.ZoomIn();
        }
    }
}
