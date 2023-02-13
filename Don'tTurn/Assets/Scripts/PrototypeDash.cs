using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeDash : MonoBehaviour
{

    //Component References
    PlayerMovement movement;
    Rigidbody2D rb;

    //Dash Variables
    public float dashSpeed;
    bool isDashing;
    bool canDash = true;
    public float dashTime;
    float dashDirection;
    int dashCount = 0;

    //Bullet Variables
    [SerializeField] GameObject BulletObj;
    [SerializeField] private GameObject bulletSpawnObj;
    GameObject currentBulletObj;
    private float bulletSpeed = 25f;
    [SerializeField] private float autoBulletDestroyTime = 0.25f;
    private bool canShoot = true;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckDash();
    }

    private void CheckDash()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            if (!isDashing)
            {
                dashDirection = Input.GetAxisRaw("Horizontal");
            }
        }

        if (!isDashing && dashCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                isDashing = true;
                dashCount--;
            }
        }

        if (movement.isGrounded && canDash)
        {
            dashCount = 1;
        }

        if (isDashing)
        {
            StartCoroutine(DashAbility());
        }
    }

    IEnumerator DashAbility()
    {
        movement.enabled = false;
        SpawnBullet();
        rb.velocity = new Vector2(dashDirection * dashSpeed, 0f);  
        yield return new WaitForSeconds(dashTime);
        movement.enabled = true;
        isDashing = false;
        canShoot = true;
    }

    private void SpawnBullet()
    {
        if (canShoot == true)
        {
            canShoot = false;
            currentBulletObj = Instantiate(BulletObj, bulletSpawnObj.transform.position, transform.rotation);
            currentBulletObj.GetComponent<Rigidbody2D>().AddRelativeForce((transform.right * -dashDirection) * bulletSpeed, ForceMode2D.Impulse);
            Destroy(currentBulletObj, autoBulletDestroyTime);
        }

    }
}
