using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float health = 0f; 
    public Image healthBar;
    public TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
        health = playerStats.health;
        healthBar.fillAmount = health / 100;
        healthText.text = health.ToString(); 

    }




}
