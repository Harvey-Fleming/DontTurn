using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureID : MonoBehaviour, IDataPersistence
{
    private CureManager cureManager;
    [SerializeField] private int cureID;
    [SerializeField] public bool hasCollectedCure;

    private void Start() 
    {
        cureManager = FindObjectOfType<CureManager>();
        if(hasCollectedCure)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemPickup, this.transform.position);
            cureManager.IncreaseCureCount();
            Destroy(gameObject);
        }
    }

    #region SaveRegion
    public void SaveData(GameData data)
    {
        if(data.hasCollectedCure.ContainsKey(cureID))
        {
            data.hasCollectedCure.Remove(cureID);
        }
        data.hasCollectedCure.Add(cureID, hasCollectedCure);
    }

    public void LoadData(GameData data)
    {
        data.hasCollectedCure.TryGetValue(cureID, out hasCollectedCure);
    }
    #endregion

}
