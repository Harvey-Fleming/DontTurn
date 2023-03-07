#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DebugButtonsEditor : EditorWindow
{

    [SerializeField] private GameObject playerObj;
    private Rigidbody2D playerRB2D;
    private BoxCollider2D playerBoxCollider2D;

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
            GetReferences();
            UnlockAllAbilities();
        }

    }

    public void GetReferences()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerRB2D = playerObj.GetComponent<Rigidbody2D>();
        playerBoxCollider2D = playerObj.GetComponent<BoxCollider2D>();
    }

    public void GodMode()
    {
        if(isGodEnabled)
        {
            gravityScale = playerRB2D.gravityScale;
            playerRB2D.gravityScale = 0;

            playerBoxCollider2D.enabled = false;

            playerObj.GetComponent<PlayerMovement>().isGodEnabled = true;
        }
        else
        {
            playerRB2D.gravityScale = gravityScale; 

            playerBoxCollider2D.enabled = true;

            playerObj.GetComponent<PlayerMovement>().isGodEnabled = false;
        }


    }

    public void UnlockAllAbilities()
    {
        UnlockAbility[] AllUnlockAbilityScript = FindObjectsOfType<UnlockAbility>();

        foreach (UnlockAbility unlockAbility in AllUnlockAbilityScript)
        {
            unlockAbility.OnAbilityUnlock();
        }
    }
}
#endif
