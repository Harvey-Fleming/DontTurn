using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUpgradeScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private AttackScript meleeAttackScript;
    private bool HasCollected = false;
    [SerializeField] private string id;

    private void FixedUpdate() 
    {
        if (HasCollected == true)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        Debug.Log("entered triggers");
        if(Input.GetKeyDown(KeyCode.W) && other.gameObject.tag == "Player")
        {
            meleeAttackScript.OnMeleeUpgrade();
            HasCollected = true;
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

}
