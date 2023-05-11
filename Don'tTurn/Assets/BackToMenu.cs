using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(MenuScreen());
    }

    IEnumerator MenuScreen()
    {
        yield return new WaitForSeconds(14f);
        SceneManager.LoadScene("Menu");
    }
   
}
