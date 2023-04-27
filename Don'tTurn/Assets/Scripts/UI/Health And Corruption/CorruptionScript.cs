using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public class CorruptionScript : MonoBehaviour
{
    //Component References
    [SerializeField] private PlayerStats playerStats;
    public TextMeshProUGUI curseText;
    private HealthBarScript healthScript; 
    public Image corruptionMetre;

    [Range(0, 100)]public float time = 0; 
    public float maxCursePoints = 100; 
    public float areaTick = 0.0048f; 
    public float metre; 
    private bool hasBeenPressedOnce;

    public float curseMutliplier;

    private void Awake() 
    {
        playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<HealthBarScript>();
        StartCoroutine(Timer(areaTick));

        time = 53;
        //start game with 53 curse
    }

    // Update is called once per frame
    void Update()
    {
        metre = time / 100;
        corruptionMetre.fillAmount = metre;
        curseText.text = "Curse Points: " + time.ToString();

        curseMutliplier = 1f + (time / 100);

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
        
        if (time <= 0)
        {
            time = 0;
        }

        if( time >= 100)
        {
            time = 100;
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

    public void OnHitCorruption(float attackDamage)
    {
        time += attackDamage;
    }

    public void OnReduceCorruption(float reduceAmount)
    {
        time -= reduceAmount;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (playerStats == null)
        {
            playerStats = GameObject.FindWithTag("Player")?.GetComponent<PlayerStats>();
        }
    }

}
