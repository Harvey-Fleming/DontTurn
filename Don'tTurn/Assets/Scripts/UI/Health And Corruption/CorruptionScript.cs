using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CorruptionScript : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    public float areaTick = 0.0048f; 
    public float stoppingPoint; //This is the point where the multiplier changes 
    public float timer;
    public float metre; 
    public Image corruptionMetre;
    private bool hasBeenPressedOnce;
    public TextMeshProUGUI curseText;
    private HealthBarScript healthScript; 

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthBarScript>(); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        metre = timer * areaTick;
        corruptionMetre.fillAmount = metre;
        float numberText = metre * 100; 
        curseText.text = "Curse Points: " + numberText.ToString(); 

        if(metre >= 1)
        {
            //decrease health here
            playerStats.health -= areaTick * 4; 
            
        }

        if(Input.GetKeyDown(KeyCode.P) && metre < 1)
        {
            timer += 35; 
        }

    }



}
