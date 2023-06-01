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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void LeaveJoshBehind()
    {
        SetChoice(2);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void LetGoOfJosh()
    {
        SetChoice(3);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void PushTheDoor()
    {
        SetChoice(4);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void Exhaustion()
    {
        SetChoice(5);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void KickTheDoor()
    {
        SetChoice(6);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void HoldOn()
    {
        SetChoice(7);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void PushOpenDoor()
    {
        SetChoice(8);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void FightWithJosh()
    {
        SetChoice(9);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
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
