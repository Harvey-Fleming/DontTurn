using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyStats))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyStats myEnemyStats = (EnemyStats)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Respawn enemy"))
        {
            myEnemyStats.isDead = false;
            myEnemyStats.Respawn();
        }
    }
}
