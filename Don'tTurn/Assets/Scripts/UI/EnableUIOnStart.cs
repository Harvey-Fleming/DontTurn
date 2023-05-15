using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUIOnStart : MonoBehaviour
{
    [SerializeField] private GameObject UICanvas;
    public GameObject fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        UICanvas.SetActive(true);
        fadeIn.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
