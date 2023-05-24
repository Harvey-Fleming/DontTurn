using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroChoiceManager : MonoBehaviour
{
    public GameObject[] choices;
    public GameObject currentChoice;
    GameObject[] instances;
    public Animator fade;


    private void Start()
    {
        fade = GetComponentInChildren<Animator>();
        fade.Play("FadeIn");
        currentChoice = Instantiate(choices[0]);
        currentChoice.transform.parent = GameObject.Find("Canvas").transform;
        currentChoice.GetComponent<RectTransform>().localPosition = Vector2.zero;
        currentChoice.transform.localScale = Vector3.one;

    }

    public void SetChoice(int i)
    {
        //fade.Play("FadeIn");
        instances = GameObject.FindGameObjectsWithTag("Choice");
        foreach (GameObject item in instances)
        {
            Destroy(item);
        }

        currentChoice = Instantiate(choices[i], transform.parent = GameObject.Find("Canvas").transform);
        currentChoice.GetComponent<RectTransform>().localPosition = Vector2.zero;
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

}
