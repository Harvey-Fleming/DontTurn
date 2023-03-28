using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Inventory : MonoBehaviour
{
    public Image medic;
    public Image mushroom;
    public TextMeshProUGUI medicText;
    public TextMeshProUGUI mushroomText;
    public int medicAmount;
    public int mushroomAmount;
    CorruptionScript corruptionScript;
    PlayerStats playerStats; 

    // Start is called before the first frame update
    void Start()
    {
        corruptionScript = GetComponent<CorruptionScript>();
        playerStats = GetComponent<PlayerStats>(); 
    }

    // Update is called once per frame
    void Update()
    {
        medicText.text = medicAmount.ToString(); 
        mushroomText.text = mushroomAmount.ToString();
        
        if(medicAmount == 0)
        {
            medic.gameObject.SetActive(false); 
        }
        else if(medicAmount >= 1)
        {
            medic.gameObject.SetActive(true);
        } 
        
        if(mushroomAmount == 0)
        {
            mushroom.gameObject.SetActive(false); 
        }
        else if(mushroomAmount >= 1)
        {
            mushroom.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && medicAmount >= 1)
        {
            UseMedKit(); 
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && mushroomAmount >= 1)
        {
            UseMushroom(); 
        }

    }

    public void UseMedKit()
    {
        playerStats.health += 10;
        medicAmount--; 
    } 
    public void UseMushroom()
    {
        corruptionScript.time -= 10;
        mushroomAmount--; 
    }


}
