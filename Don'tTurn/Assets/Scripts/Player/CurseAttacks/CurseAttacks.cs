using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseAttacks : MonoBehaviour
{
    public GameObject bomb;
    public Transform firePoint;
    public GameObject eatTrigger; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(); 
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(EatEnemy()); 
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
}
