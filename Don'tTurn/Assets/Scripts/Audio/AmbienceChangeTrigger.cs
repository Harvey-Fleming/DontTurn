using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;
    [SerializeField] private string parameterName2;
    [SerializeField] private float parameterValue2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
            Debug.Log("change city ambience");
            AudioManager.instance.SetAmbienceParameter(parameterName2, parameterValue2);
            Debug.Log("change sewer ambience");
        }
    }
}
