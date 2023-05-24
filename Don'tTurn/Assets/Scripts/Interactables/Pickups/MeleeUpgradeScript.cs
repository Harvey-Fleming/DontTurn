using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUpgradeScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private AttackScript meleeAttackScript;
    private bool HasCollected = false;
    [SerializeField] private string id;
    private PlayerInput playerInput;
    bool onHover;
    private IntroTutorialManager tutorial;
    float startY;
    public float bobAmount;
    public float bobSpeed;

    private void Update() 
    {
        transform.position = new Vector3(transform.position.x, startY + (Mathf.Sin(Time.time * bobSpeed) * bobAmount / 1000f), transform.position.z);

        if (HasCollected == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            tutorial.MeleeUpgrageTutorial();
            meleeAttackScript.OnMeleeUpgrade();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemPickup, this.transform.position);
            HasCollected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !HasCollected)
        {
            onHover = true;
        }
    }

    public void SaveData(GameData gameData)
    {
        if(gameData.hasCollectedUpgrade.ContainsKey(id))
        {
            gameData.hasCollectedUpgrade.Remove(id);
        }
        gameData.hasCollectedUpgrade.Add(id, HasCollected);
    }

    public void LoadData(GameData gameData)
    {
        gameData.hasCollectedUpgrade.TryGetValue(id, out HasCollected);
    }

    private void Start()
    {
        if (HasCollected)
        {
            meleeAttackScript.OnMeleeUpgrade();
        }

        startY = transform.position.y;

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        tutorial = GameObject.Find("IntroTutorial").GetComponent<IntroTutorialManager>();
    }
}
