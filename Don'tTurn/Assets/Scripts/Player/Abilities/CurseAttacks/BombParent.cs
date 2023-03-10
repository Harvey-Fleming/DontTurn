using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
