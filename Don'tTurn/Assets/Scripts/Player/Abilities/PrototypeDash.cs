using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(PlayerInput))]
public class PrototypeDash : MonoBehaviour, IDataPersistence
{

    //Component References
    private PlayerMovement movement;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInput playerInput;
    private CursorScript cursorScript;

    private Vector2 cursorPos;

    //Dash Variables
    [Header("Dash Variables")]
    public bool isUnlocked = false;
    [SerializeField] private float dashSpeed;
    public bool isDashing;
    private bool canDash = true;
    [SerializeField] private float dashTime, dashCooldown = 2;
    private float dashDirection;
    private int dashCount = 0;

    //Bullet Variables
    [Header("Bullet Variables")]
    [SerializeField] GameObject Bullet;
    [SerializeField] private GameObject bulletSpawnObj, rightSpawn, leftSpawn;
    [SerializeField] private float autoBulletDestroyTime = 0.25f;
    private float bulletSpeed = 25f;
    private bool canShoot = true;


    [Header("UI")]
    public GameObject shotgunUI;
    public Image circle;
    public Image shells; 
    float timer;
    public Camera mainCamera;
    bool startCooldown;

    private void Start() 
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cursorScript = GameObject.Find("Cursor").GetComponent<CursorScript>();
        rightSpawn = bulletSpawnObj.transform.GetChild(0).gameObject;
        leftSpawn = bulletSpawnObj.transform.GetChild(1).gameObject;

    }
    
    void Update()
    {
        //Only allows Dash to be performed if it has been picked up from NPC
        if (isUnlocked)
        {
            CheckDash();
        }

        if (isDashing)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        cursorPos = cursorScript.newCursorPos;

        if (startCooldown)
        {
            shotgunUI.SetActive(true);
            timer += Time.deltaTime;
            circle.fillAmount = (dashCooldown - timer) / dashCooldown;
        }
        else
        {
            shotgunUI.SetActive(false);
            timer = 0;
        }
    }

    private void CheckDash()
    {
        if (!isDashing && dashCount > 0)
        {
            if (playerInput.moveAbilityInput && playerInput.dashSelected && canDash)
            {
                dashCount--;
                isDashing = true;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.shotgunFire, this.transform.position);
            }
        }

        if (canDash) { dashCount = 1; }       
        
        if (isDashing) {  StartCoroutine(DashAbility()); }
    }

    IEnumerator DashAbility()
    {
        if (canDash == true && canShoot == true)
        {
            //Changes where the bullet will spawn based on the direction the player is facing due to x Scale also affecting bullet spawns
            //For example, when the player is facing left, the x scale is -1 and the 'right' bullet spawn location will actually be on the left, leaing to bullet and player colliding and nothing happening.
            DecideBulletSpawn();
            FindObjectOfType<AttackScript>().ResetWindow();
            canDash = false;
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetBool("IsDashing", true);
            movement.enabled = false;
            SpawnBullet();
            rb.velocity = new Vector2(-dashDirection * dashSpeed, 0f);
            yield return new WaitForSeconds(dashTime);
            startCooldown = true;
            rb.velocity = new Vector2(0, 0);
            movement.enabled = true;
            isDashing = false;
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool("IsDashing", false);

            //Initiate Dash Cooldown
            yield return new WaitForSeconds(dashCooldown);
            startCooldown = false;
            canDash = true;
            canShoot = true;
            yield break;
        }
    }

    private void DecideBulletSpawn()
    {
        switch (movement.facingright)
        {
            case true:
                if (cursorPos.x < transform.position.x && movement.facingright)
                {
                    dashDirection = -1;
                    bulletSpawnObj = leftSpawn;
                }
                else if (cursorPos.x > transform.position.x && movement.facingright)
                {
                    dashDirection = 1;
                    bulletSpawnObj = rightSpawn;
                }
                break;
            case false:
                if (cursorPos.x < transform.position.x && !movement.facingright)
                {
                    dashDirection = -1;
                    bulletSpawnObj = rightSpawn;
                }
                else if (cursorPos.x > transform.position.x && !movement.facingright)
                {
                    dashDirection = 1;
                    bulletSpawnObj = leftSpawn;
                }
                break;
        }
    }

    private void SpawnBullet()
    {
        if (canShoot == true)
        {
            canShoot = false;
            GameObject  currentBulletObj = Instantiate(Bullet, bulletSpawnObj.transform.position, transform.rotation);
            currentBulletObj.GetComponent<Rigidbody2D>().AddRelativeForce((-transform.right * -dashDirection) * bulletSpeed, ForceMode2D.Impulse);
            Destroy(currentBulletObj, autoBulletDestroyTime);
        }
    }

    //Save and Loading Data
    public void LoadData(GameData data)
    {
        this.isUnlocked = data.isShotgunUnlocked;
    }

    public void SaveData(GameData data)
    {
        data.isShotgunUnlocked = this.isUnlocked;
    }
}