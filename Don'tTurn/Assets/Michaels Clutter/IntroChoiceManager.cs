using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroChoiceManager : MonoBehaviour
{
    public GameObject[] choices;
    public GameObject fade;


    public void Start()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if(i == 0)
            {
                choices[i].SetActive(true);
            }
            else
            {
                choices[i].SetActive(false);
            }
        }
    }

    public void SetChoice(int choiceNum)
    {
        fade.SetActive(false);
        fade.SetActive(true);
        StartCoroutine(DialogueDelay(choiceNum));
    }

    public void HelpJoshUp()
    {
        SetChoice(1);
    }

    public void LeaveJoshBehind()
    {
        SetChoice(2);
    }

    public void LetGoOfJosh()
    {
        SetChoice(3);
    }

    public void PushTheDoor()
    {
        SetChoice(4);
    }

    public void Exhaustion()
    {
        SetChoice(5);
    }

    public void KickTheDoor()
    {
        SetChoice(6);
    }

    public void HoldOn()
    {
        SetChoice(7);
    }

    public void PushOpenDoor()
    {
        SetChoice(8);
    }

    public void FightWithJosh()
    {
        SetChoice(9);
    }

    public void FindACure()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator DialogueDelay(int choiceNum)
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < choices.Length; i++)
        {
            if (i == choiceNum)
            {
                choices[i].SetActive(true);
            }
            else
            {
                choices[i].SetActive(false);
            }
        }
    }

}
