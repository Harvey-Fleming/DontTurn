#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DebugButtonsEditor : EditorWindow
{

    [SerializeField] private GameObject playerObj;
    private Rigidbody2D playerRB2D;
    private BoxCollider2D playerBoxCollider2D;
    private PlayerMovement playerMovement;

    private float gravityScale;

    private bool isGodEnabled = false;

    [MenuItem("Window/Debug Buttons")]
    public static void ShowWindow()
    {
        GetWindow<DebugButtonsEditor>("Debug Buttons");
    }

    void OnGUI()
    {
        GUILayout.Label("Disables Gravity, player Collisions and allows free movement.", EditorStyles.miniBoldLabel);
        if(GUILayout.Button("Enable God Mode"))
        {
            //Gets reference to the player and required components, tells the script whether
            GetReferences();
            isGodEnabled = !isGodEnabled;
            GodMode();
        }

        GUILayout.Label("Unlocks All Abilities", EditorStyles.miniBoldLabel);
        if(GUILayout.Button("Unlock Abilities"))
        {
            UnlockAllAbilities();
        }

        GUILayout.Label("Respawns All Enemies in the Level", EditorStyles.miniBoldLabel);
        if(GUILayout.Button("Respawn Enemies"))
        {
            RespawnAllEnemies();
        }

        GUILayout.Label("Teleport to Mushroom Area", EditorStyles.miniBoldLabel);
        if(GUILayout.Button("Mushroom TP"))
        {
            TeleportPlayer();
        }

    }

    private void TeleportPlayer()
    {
        GetReferences();
        playerObj.transform.position = new Vector3(355, 25.54f, 0);
    }

    public void RespawnAllEnemies()
    {
        object[] allEnemies = Resources.FindObjectsOfTypeAll(typeof(EnemyStats));

        foreach (EnemyStats enemyStats in allEnemies)
        {
            enemyStats.gameObject.SetActive(true);
            enemyStats.isDead = false;
            enemyStats.Respawn();
        }
    }

    public void GetReferences()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerRB2D = playerObj.GetComponent<Rigidbody2D>();
        playerBoxCollider2D = playerObj.GetComponent<BoxCollider2D>();
        playerMovement = playerObj.GetComponent<PlayerMovement>();
    }

    public void GodMode()
    {
        if(isGodEnabled)
        {
            gravityScale = playerRB2D.gravityScale;
            playerRB2D.gravityScale = 0;

            playerBoxCollider2D.enabled = false;

            playerMovement.isGodEnabled = true;
            playerMovement.moveSpeed *= 2;
        }
        else
        {
            playerRB2D.gravityScale = gravityScale; 

            playerBoxCollider2D.enabled = true;

            playerMovement.isGodEnabled = false;
            playerMovement.moveSpeed /= 2;
        }


    }

    public void UnlockAllAbilities()
    {
        UnlockAbility[] allUnlockAbilityScript = FindObjectsOfType<UnlockAbility>();

        foreach (UnlockAbility unlockAbility in allUnlockAbilityScript)
        {
            unlockAbility.OnAbilityUnlock();
        }
    }
}
#endif
