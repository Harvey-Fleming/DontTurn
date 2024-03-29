using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

public class IntroTutorialManager : MonoBehaviour, IDataPersistence
{
    public float fadeOutTime;
    public GameObject Player;
    bool canSkipDialogue = true;

    [Header("Movement Icon")]
    public GameObject movementIcon;
    Vector3 movementIconStartPos;
    float bobSpeed = 1f;
    float bobAmount = 0.2f;
    float elapsedTime;

    [Header("Medkit")]
    public Inventory inventory;
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
    [SerializeField] int curseStringNum = 0;

    [Header("Checkpoint")]
    bool hasUsedCheckpoint;
    public PlayerCollision playerCollision;
    public GameObject mapIcon;
    public GameObject checkpointTutorial;
    public Text checkpointText;
    public string[] checkpointStrings;
    int checkpointStringNum;

    [Header("Melee Upgrade")]
    bool hasPickedUpMelee;
    public GameObject meleeUpgPrompt;
    bool canContinueMelee = false;
    bool hasUsedMelee = false;

    void Start()
    {
        movementIconStartPos = movementIcon.transform.position;
        spaceBarIconStartPos = spaceBarIcon.transform.position;
        mouseIconStartPos = mouseIcon.transform.position;
        player = GameObject.Find("Player");

        if(hasUsedCheckpoint == false)
        {
            mapIcon.SetActive(false);
        }
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

        CheckpointTutorial();

        if (hasPickedUpMelee)
        {
            if (Input.GetKeyDown(KeyCode.F) && canContinueMelee && !hasUsedMelee)
            {
                Time.timeScale = 1;
                meleeUpgPrompt.SetActive(false);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
                hasUsedMelee = true;
            }
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
        if(inventory.medicAmount >= 1 && !pauseFinished)
        {
            Time.timeScale = 0;
            Player.GetComponent<PlayerMovement>().playerFootsteps.stop(STOP_MODE.IMMEDIATE);
            medkitPrompt.SetActive(true);
            pauseFinished = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && pauseFinished)
        {
            Time.timeScale = 1;
            medkitPrompt.SetActive(false);
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
        if (curseStringNum < (curseStrings.Length - 1))
        {
            Time.timeScale = 0;
            Player.GetComponent<PlayerMovement>().playerFootsteps.stop(STOP_MODE.IMMEDIATE);
            curseText.text = curseStrings[curseStringNum];
            curseTutorial.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && canSkipDialogue)
            {
                StartCoroutine(TextCooldown());
                curseStringNum++;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
            }
        }
        if(curseStringNum >= (curseStrings.Length - 1))
        {
            curseText.text = curseStrings[curseStringNum];
            if (Input.GetKeyDown(KeyCode.F) && canSkipDialogue)
            {
                Time.timeScale = 1;
                curseTutorial.SetActive(false);
            }
        }
    }

    void CheckpointTutorial()
    {
        if(!hasUsedCheckpoint && playerCollision.isInsideTrigger && !playerCollision.IsMovingToTarget && playerCollision.interactPressed)
        {
            checkpointText.text = checkpointStrings[checkpointStringNum];

            if (checkpointStringNum < checkpointStrings.Length - 1)
            {
                Time.timeScale = 0;
                playerCollision.canStandUp = false;
                checkpointTutorial.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F) && canSkipDialogue)
                {
                    StartCoroutine(TextCooldown());
                    checkpointStringNum++;
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
                }
            }
            if (checkpointStringNum >= checkpointStrings.Length - 1)
            {
                if (Input.GetKeyDown(KeyCode.F) && canSkipDialogue)
                {
                    StartCoroutine(CheckpointCooldown());
                    Time.timeScale = 1;
                    playerCollision.canStandUp = true;
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
                    checkpointTutorial.SetActive(false);
                    mapIcon.SetActive(true);
                    hasUsedCheckpoint = true;
                }
            }
        }
    }

    public void MeleeUpgrageTutorial()
    {
        if (!hasPickedUpMelee)
        {
            Player.GetComponent<PlayerMovement>().playerFootsteps.stop(STOP_MODE.IMMEDIATE);
            StartCoroutine(MeleeCooldown());
            meleeUpgPrompt.SetActive(true);
            Time.timeScale = 0;
            hasPickedUpMelee = true;
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

    IEnumerator TextCooldown()
    {
        canSkipDialogue = false;
        yield return new WaitForSecondsRealtime(0.2f);
        canSkipDialogue = true;
    }

    IEnumerator CheckpointCooldown()
    {
        playerCollision.enabled = false;
        yield return new WaitForSeconds(0.1f);
        playerCollision.enabled = true;
    }

    IEnumerator MeleeCooldown()
    {
        canContinueMelee = false;
        yield return new WaitForSecondsRealtime(0.2f);
        canContinueMelee = true;
    }

        #region SaveRegion
    public void SaveData(GameData data)
    {
        data.hasUsedCheckpoint = this.hasUsedCheckpoint;
    }

    public void LoadData(GameData data)
    {
        this.hasUsedCheckpoint = data.hasUsedCheckpoint;
    }

    #endregion
}
