using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAttacks : MonoBehaviour
{
    public GameObject bomb;
    public Transform firePoint;
    public GameObject eatTrigger;
    public GameObject PunchHitbox;
    private PlayerStats playerStats;
    private CorruptionScript CorruptionScript; 
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>(); 
        CorruptionScript = GetComponent<CorruptionScript>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerStats.OnHit(15, gameObject); 
            Shoot(); 
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            EatEnemyFunction(); 
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CorruptionScript.time += 10; 
           PunchEnemy(); 
        }
    }

    public void Shoot()
    {
        Instantiate(bomb, firePoint.position, firePoint.rotation); 
    }

    public IEnumerator EatEnemy()
    {
        eatTrigger.SetActive(true);

        yield return new WaitForSeconds(1f);

        eatTrigger.SetActive(false); 
    }

    public void EatEnemyFunction()
    {
        eatTrigger.SetActive(true); 
    }

    public void PunchEnemy()
    {
        PunchHitbox.SetActive(true); 
    }
}
