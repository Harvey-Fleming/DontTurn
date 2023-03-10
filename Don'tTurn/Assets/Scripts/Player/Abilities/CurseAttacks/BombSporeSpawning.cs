using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSporeSpawning : MonoBehaviour
{
    public GameObject sporePrefab;
    public float radius = 1f;

    public void SpawnObjectAtRandom()
    {
        Vector3 randomPos = Random.insideUnitCircle * radius;
        
        Instantiate(sporePrefab, randomPos, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
