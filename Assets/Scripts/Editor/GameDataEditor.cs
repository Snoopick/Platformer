using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class GameData
{
    [System.Serializable]
    public class LevelData
    {
        public string LevelNames;
        public string Difficulty;
    }
    
    public LevelData[] LevelslData;


}

public class GameDataEditor : EditorWindow
{
    public GameData gameData;
    private string projectGameDataPath = "/Data/GameData.json";

    [MenuItem("Tools/Game Data Editor")]
    private static void Init()
    {
        GetWindow(typeof(GameDataEditor)).Show();
    }

    private void OnGUI()
    {
        if (gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveGameData();
            }
        }
        
        if (GUILayout.Button("Load Data"))
        {
            LoadGameData();
        }

        
    }
    
    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = $"{Application.dataPath}{projectGameDataPath}";

        File.WriteAllText(filePath, dataAsJson);
        AssetDatabase.Refresh();
    }
    
    private void LoadGameData()
    {
        string filePath = $"{Application.dataPath}{projectGameDataPath}";
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }
}
