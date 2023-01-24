using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CorruptionScript : MonoBehaviour
{
    private bool hasStopped; 
    public float stoppingPoint; //This is the point where the multiplier changes 
    public float multiplier; 
    public float test; 
    public float timer;
    public float metre; 
    public Image corruptionMetre;
    private bool hasBeenPressedOnce;
    public TextMeshProUGUI healthText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        corruptionMetre.fillAmount = timer * 0.1f;






        if(Input.GetKeyDown(KeyCode.P) && hasBeenPressedOnce == true)
        {
            PowerUpOff(); 
        }
        else if (Input.GetKeyDown(KeyCode.P) && hasBeenPressedOnce == false)
        {
            PowerUpOn(); 
        }
        if(Input.GetKeyDown(KeyCode.P))
        {

            stoppingPoint = metre;
            metre = stoppingPoint; 
        }
    }

    public void PowerUpOn()
    {
        hasBeenPressedOnce = true;
        stoppingPoint = timer; 
    }
    public void PowerUpOff()
    {
        hasBeenPressedOnce = false; 
    }
}
