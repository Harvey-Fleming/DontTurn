using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class CorruptionScript : MonoBehaviour
{
    public float time = 0; 
    [SerializeField] private PlayerStats playerStats;
    public float areaTick = 0.0048f; 
    public float metre; 
    public Image corruptionMetre;
    private bool hasBeenPressedOnce;
    public TextMeshProUGUI curseText;
    private HealthBarScript healthScript; 


    private void Awake() 
    {
        playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthBarScript>();
        StartCoroutine(Timer(areaTick)); 
    }

    // Update is called once per frame
    void Update()
    {
        metre = time / 100;
        corruptionMetre.fillAmount = metre;
        curseText.text = "Curse Points: " + time.ToString(); 



        if(Input.GetKeyDown(KeyCode.P))
        {
            if(time > 35)
            {
                time -= 35;
            }
            else if (time < 50)
            {
                playerStats.health -= 35; 
            }
           
        }

    }

    public IEnumerator Timer(float tick)
    {
        while(time<100)
        {
            time++;
            yield return new WaitForSeconds(tick); 
        }
        while(time >= 100)
        {
            playerStats.health--;
            yield return new WaitForSeconds(tick/4);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerStats == null)
        {
            playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        }
    }




}
