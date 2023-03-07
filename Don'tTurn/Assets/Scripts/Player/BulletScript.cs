using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private int basebulletDamage = 10, minbulletDamage = 1;
    [SerializeField] private float bulletDamage, bulletTravelTime, bulletTravelFalloff = 0.25f;
    

    private void Awake() 
    {
        bulletTravelTime = 0f;
    }

    private void Update() 
    {
        bulletTravelTime += Time.deltaTime;
    }
    // Start is called before the first frame update
    void OnValidate()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private float CalculateBulletDamage()
    {
        bulletDamage = Mathf.Lerp(basebulletDamage, minbulletDamage, bulletTravelTime/bulletTravelFalloff);
        Debug.Log("Damage Dealt to enemy: " + basebulletDamage);
        return bulletDamage;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            CalculateBulletDamage();
            other.gameObject.GetComponent<EnemyStats>()?.OnHit(Mathf.RoundToInt(bulletDamage));
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }

}
