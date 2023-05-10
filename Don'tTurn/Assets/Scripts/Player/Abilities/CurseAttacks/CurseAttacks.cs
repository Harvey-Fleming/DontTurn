using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class CurseAttacks : MonoBehaviour, IDataPersistence
{
    Animator animator; 
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
        animator = GetComponent<Animator>(); 
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
                if (CorruptionScript.time < 100f)
                {
                    CorruptionScript.time += 10;
                    PunchEnemy();
                }
                else
                {
                    playerStats.OnHit(10, gameObject);
                    PunchEnemy();
                }

                StartCoroutine(CursePunchAnimation()); 

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

    //Save and Loading Data
    public void LoadData(GameData data)
    {
        this.isBombUnlocked = data.isBombUnlocked;
        this.isCursePunchUnlocked = data.isCursePunchUnlocked;
        this.isEatUnlocked = data.isEatUnlocked;
    }

    public void SaveData(GameData data)
    {
        data.isBombUnlocked = this.isBombUnlocked;
        data.isCursePunchUnlocked = this.isCursePunchUnlocked;
        data.isEatUnlocked = this.isEatUnlocked;
        
    }

    public IEnumerator CursePunchAnimation()
    {
        animator.SetBool("IsCursePunching", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsCursePunching", false);
    }
}
