using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorDestroyScript : MonoBehaviour
{
    private float timespawned;
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        timespawned = Time.realtimeSinceStartup;
        text.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(20f, -20f));
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.realtimeSinceStartup - timespawned) > 0.1)
        {
            Destroy(this.gameObject);
        }
    }
}
