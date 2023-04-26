using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseTutorialButton : MonoBehaviour
{
    public GameObject panel;
    public int tutorialNumber;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatePanel() //
    {
        panel.SetActive(true);
        panel.GetComponent<CurseTutorialPanel>().tutorialNumber = tutorialNumber;
        Time.timeScale = 0;


    }
}
