using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    //Component References
    [SerializeField] private GameObject DamageIndicatorPrefab;
    [SerializeField] private Transform damageIndicatorSpawnPos;

    private GameObject spawnedDamageIndicator;
    private TMP_Text damageIndicatorText;

    public void SpawnIndicator(float attackDamage, Color textColour)
    {
        spawnedDamageIndicator = Instantiate(DamageIndicatorPrefab, damageIndicatorSpawnPos.position, Quaternion.identity);
        damageIndicatorText = spawnedDamageIndicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        StartCoroutine(DamageIndication(attackDamage, textColour));
    }

    private IEnumerator DamageIndication(float attackDamage, Color textColour)
    {
        damageIndicatorText.text = attackDamage.ToString(); 
        damageIndicatorText.color = textColour;
        damageIndicatorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Destroy(spawnedDamageIndicator);
        yield break; 
    }

}
