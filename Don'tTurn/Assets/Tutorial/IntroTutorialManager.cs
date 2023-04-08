using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutorialManager : MonoBehaviour
{
    public float fadeOutTime;

    [Header("Movement Icon")]
    public GameObject movementIcon;
    Vector3 movementIconStartPos;
    float bobSpeed = 1f;
    float bobAmount = 0.2f;
    float elapsedTime;

    [Header("Medkit")]
    public MedkitCounter medkitCounter;
    public GameObject medkitPrompt;
    public bool pauseFinished = false;

    [Header("Jump")]
    public GameObject spaceBarIcon;
    Vector3 spaceBarIconStartPos;
    GameObject player;
    float spaceBarElapsedTime;

    [Header("Mouse")]
    public GameObject mouseIcon;
    Vector3 mouseIconStartPos;
    float mouseElapsedTime;

    [Header("Curse")]
    bool curseTutorialShow;
    public EnemyStats enemy;
    public GameObject curseTutorial;
    public Text curseText;
    public string[] curseStrings;
    int curseStringNum = 0;

    void Start()
    {
        movementIconStartPos = movementIcon.transform.position;
        spaceBarIconStartPos = spaceBarIcon.transform.position;
        mouseIconStartPos = mouseIcon.transform.position;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        MovementIconBob();

        SpaceBarIcon();

        MouseIconBob();

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MoveIconFade());
        }

        MedkitTutorial();

        if (enemy.isDead)
        {
            CurseTutorial();
        }
    }

    void MovementIconBob()
    {
        if (movementIcon)
        {
            movementIcon.transform.position = new Vector3(movementIconStartPos.x, movementIconStartPos.y + (Mathf.Sin(Time.time * bobSpeed) * bobAmount), movementIconStartPos.z);
        }
    }

    void MedkitTutorial()
    {
        if(medkitCounter.medkitAmount == 1 && !pauseFinished)
        {
            Time.timeScale = 0;
            medkitPrompt.SetActive(true);
            pauseFinished = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && pauseFinished && medkitCounter.medkitAmount > 0)
        {
            Time.timeScale = 1;
            medkitPrompt.SetActive(false);
            player.GetComponent<PlayerStats>().health += 25;
            medkitCounter.medkitAmount--;
            medkitCounter.medkitText.text = medkitCounter.medkitAmount.ToString();
        }
    }
    
    void SpaceBarIcon()
    {
        if (spaceBarIcon)
        {
            spaceBarIcon.transform.position = new Vector3(spaceBarIconStartPos.x, spaceBarIconStartPos.y + (Mathf.Sin(Time.time * bobSpeed) * bobAmount), spaceBarIconStartPos.z);
        }

        if(Vector3.Distance(player.transform.position, spaceBarIconStartPos) < 5f && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpaceBarIconFade());
        }
    }

    void MouseIconBob()
    {
        if (mouseIcon)
        {
            mouseIcon.transform.position = new Vector3(mouseIconStartPos.x, mouseIconStartPos.y + (Mathf.Sin(Time.time * bobSpeed) * bobAmount), mouseIconStartPos.z);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && Vector3.Distance(player.transform.position, mouseIconStartPos) < 5f)
        {
            StartCoroutine(MousesIconFade());
        }
    }

    void CurseTutorial()
    {
        if (curseStringNum < curseStrings.Length)
        {
            Time.timeScale = 0;
            curseTutorial.SetActive(true);
            curseText.text = curseStrings[curseStringNum];
            if (Input.GetKeyDown(KeyCode.Return))
            {
                curseStringNum++;
            }
        }
        if(curseStringNum >= curseStrings.Length - 1)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                curseTutorial.SetActive(false);
            }
        }
    }


    IEnumerator MoveIconFade()
    {
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);

            movementIcon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a);

            yield return null;
        }
        Destroy(movementIcon);
    }

    IEnumerator SpaceBarIconFade()
    {
        while (spaceBarElapsedTime < fadeOutTime)
        {
            spaceBarElapsedTime += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, spaceBarElapsedTime / fadeOutTime);

            spaceBarIcon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a);

            yield return null;
        }
        Destroy(spaceBarIcon);
    }

    IEnumerator MousesIconFade()
    {
        while (mouseElapsedTime < fadeOutTime)
        {
            mouseElapsedTime += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, mouseElapsedTime / fadeOutTime);

            mouseIcon.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a);

            yield return null;
        }
        Destroy(mouseIcon);
    }
}