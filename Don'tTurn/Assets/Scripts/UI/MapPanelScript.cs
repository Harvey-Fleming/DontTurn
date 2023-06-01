using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MapPanelScript : MonoBehaviour
{
    public Image[] images; 
    public MapScript MapScript;
    public GameObject icon;
    public Vector3[] iconLoc;

    // Start is called before the first frame update
    void Start()
    {
        icon.SetActive(false);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    public void ShowMap(int mapNum)
    {
        icon.SetActive(true);
        images[mapNum - 1].gameObject.SetActive(true);
        icon.transform.localPosition = iconLoc[mapNum - 1];
    }
}
