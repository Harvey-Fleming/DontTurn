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

    private void Update() 
    {
        if (HasCollected == true)
        {
            Destroy(this.gameObject);
        }

        if (onHover && playerInput.interactInput && !HasCollected)
        {
            meleeAttackScript.OnMeleeUpgrade();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemPickup, this.transform.position);
            HasCollected = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            onHover = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }
}
