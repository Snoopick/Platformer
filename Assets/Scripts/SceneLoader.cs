using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private static string nextLevel;
    public Animator animator;

    public static void LoadLevel(string level)
    {
        nextLevel = level;
        SceneManager.LoadScene("LoadingScene");
    }
    
    
    IEnumerator Start()
    {
        GameManager.SetGameState(GameState.Loading);
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        
        if (string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene("MainMenu");
            yield break;
        }

        AsyncOperation loading = null;
        loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

        while (!loading.isDone)
        {
            yield return null;
        }

        nextLevel = null;
        
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}
