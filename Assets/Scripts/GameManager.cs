using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Loading,
    Game,
    GamePause
}

public class GameManager : MonoBehaviour
{
    private static GameState currentGameState;
    public static Action<GameState> GameStateAction;

    public static GameState GetGameState => currentGameState;

    public IPlayer Player;
    public List<IEnemy> Enemies = new List<IEnemy>();

    public static void SetGameState(GameState newGemaState)
    {
        currentGameState = newGemaState;
        GameStateAction?.Invoke(newGemaState);
    }
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.Game);
        print(Player);
        print($"Enemies count {Enemies.Count}");
    }
    
#if UNITY_EDITOR
    [ContextMenu("Hide all characters")]
    public void HideAllCharacters()
    {
        var characters = FindCharacters();
        foreach (var character in characters)
        {
            character.SetActive(false);
        }
    }
    
    [ContextMenu("Show all characters")]
    public void ShowAllCharacters()
    {
        var characters = FindCharacters();
        foreach (var character in characters)
        {
            character.SetActive(true);
        }
    }

    public List<GameObject> FindCharacters()
    {
        var characters = Resources.FindObjectsOfTypeAll<BaseEnemy>().Select(e => e.gameObject).ToList();
        characters.AddRange(Resources.FindObjectsOfTypeAll<Player>().Select(p => p.gameObject).ToList());

        return characters;
    }
    
#endif
}
