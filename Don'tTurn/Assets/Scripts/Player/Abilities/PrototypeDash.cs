using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeDash : MonoBehaviour
{

    //Component References
    PlayerMovement movement;
    Rigidbody2D rb;
    Animator animator;

    //Dash Variables
    [Header("Dash Variables")]
    public bool isUnlocked  = false;
    [SerializeField] private float dashSpeed;
    bool isDashing;
    bool canDash = true;
    [SerializeField] private float dashTime;
    float dashDirection;
    int dashCount = 0;
    [SerializeField] private float dashCooldown = 2;

    //Bullet Variables
    [Header("Bullet Variables")]
    [SerializeField] GameObject BulletObj;
    [SerializeField] private GameObject bulletSpawnObj;
    [SerializeField] private float autoBulletDestroyTime = 0.25f;
    private float bulletSpeed = 25f;
    private bool canShoot = true;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Only allows Dash to be performed if it has been picked up from NPC
        if (isUnlocked)
        {
            CheckDash();
        }
    }

    private void CheckDash()
    {
        if (!isDashing && dashCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                dashCount--;

                isDashing = true; 
            }
        }

        if (movement.IsGrounded() && canDash)
        {
            dashCount = 1;          
        }

        if (isDashing)
        {
            canDash = false;
            StartCoroutine(DashAbility());        
        }
    }

    IEnumerator DashAbility()
    {
        if (canDash == false && canShoot == true)
        {
            dashDirection = gameObject.transform.localScale.x;
            animator.SetBool("IsDashing", true);
            movement.enabled = false;
            SpawnBullet();
            rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);  
            yield return new WaitForSeconds(dashTime);
            rb.velocity = new Vector2(0, 0);
            movement.enabled = true; 
            isDashing = false;
            animator.SetBool("IsDashing", false);
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
            canShoot = true;
            yield break;
        }
    }

    private void SpawnBullet()
    {
        if (canShoot == true)
        {
            canShoot = false;
            GameObject  currentBulletObj = Instantiate(BulletObj, bulletSpawnObj.transform.position, transform.rotation);
            currentBulletObj.GetComponent<Rigidbody2D>().AddRelativeForce((transform.right * -dashDirection) * bulletSpeed, ForceMode2D.Impulse);
            Destroy(currentBulletObj, autoBulletDestroyTime);
        }
    }
}