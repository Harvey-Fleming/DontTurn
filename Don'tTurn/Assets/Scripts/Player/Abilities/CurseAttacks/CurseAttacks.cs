using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class CurseAttacks : MonoBehaviour
{
    [SerializeField] bool isOnCooldown; 
    public GameObject bomb;
    public Transform firePoint;
    public GameObject eatTrigger, PunchHitbox;
    private PlayerStats playerStats;
    public CorruptionScript CorruptionScript; 
    private PlayerInput playerInput;

    public bool isCursePunchUnlocked;
    public bool isBombUnlocked;
    public bool isEatUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnCooldown == false)
        {
            if (playerInput.bombAbilityInput && isBombUnlocked)
            {
                isOnCooldown = true;
                playerStats.OnHit(15, gameObject);
                Shoot();
                
            }
            if (playerInput.eatAbilityInput && isEatUnlocked)
            {
                isOnCooldown = true;
                EatEnemyFunction();
               
            }
            if (playerInput.punchAbilityInput && isCursePunchUnlocked)
            {
                isOnCooldown = true;
                Debug.Log("Left shift!!");
                if (CorruptionScript.time >= 10f)
                {
                    CorruptionScript.time -= 10;
                    PunchEnemy();
                }
                else
                {
                    playerStats.OnHit(10, gameObject);
                    PunchEnemy();

                }
                


            }
        }
        else
        {
            StartCoroutine(Cooldown()); 
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

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        isOnCooldown = false; 
    }
}
