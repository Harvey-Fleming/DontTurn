using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorDestroyScript : MonoBehaviour
{
    private float timespawned;

    // Start is called before the first frame update
    void Start()
    {
        timespawned = Time.realtimeSinceStartup;
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
