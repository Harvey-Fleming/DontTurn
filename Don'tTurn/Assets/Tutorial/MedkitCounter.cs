using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedkitCounter : MonoBehaviour
{
    public int medkitAmount = 0;
    [HideInInspector] public Text medkitText;

    private void Start()
    {
        medkitText = GameObject.Find("MedkitNum").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Medkit")
        {
            medkitAmount++;
            medkitText.text = medkitAmount.ToString();
            Destroy(collision.gameObject);
        }
    }
}
