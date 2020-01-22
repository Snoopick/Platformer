using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameControllerEditorExtention : Editor
{
    [MenuItem("Tools/Main/Select Game Manager %g")]
    public static void SelectGameManager()
    {
        var manager = FindObjectOfType<GameManager>();

        if (manager)
        {
            Selection.objects = new Object[] {manager.gameObject};
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var myScript = (GameManager) target;
        
        GUILayout.Space(20);
        
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Hide characters", GUILayout.Width(120), GUILayout.Height(20)))
        {
            myScript.HideAllCharacters();
        }
        
        if (GUILayout.Button("Show characters", GUILayout.Width(120), GUILayout.Height(20)))
        {
            myScript.ShowAllCharacters();
        }
        
        GUILayout.EndHorizontal();
    } 
}
