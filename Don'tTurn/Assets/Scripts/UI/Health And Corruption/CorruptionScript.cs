using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CorruptionScript : MonoBehaviour
{
    public float areaTick = 0.024f; 
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
        corruptionMetre.fillAmount = timer * areaTick;
        curseText.text = "Curse Points: " + metre.ToString(); 





        if(Input.GetKeyDown(KeyCode.P) && hasBeenPressedOnce == true)
        {
            PowerUpOff(); 
        }
        else if (Input.GetKeyDown(KeyCode.P) && hasBeenPressedOnce == false)
        {
            PowerUpOn(0.003f); 
        }

        if(metre >= 1)
        {
            //decrease health here
            
        }

    }

    public void PowerUpOn(float cursePoints)
    {
        hasBeenPressedOnce = true;
        timer += cursePoints; 
        //stoppingPoint = timer; 
    }
    public void PowerUpOff()
    {
        hasBeenPressedOnce = false; 
    }
}
