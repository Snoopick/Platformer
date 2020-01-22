using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    
    [SerializeField] private Button loadLevelButton; 
    [SerializeField] private Button settingsButton; 
    [SerializeField] private Button closeSettings;

    [SerializeField] private InputField nicknameInputField;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.SetGameState(GameState.MainMenu);
        
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        
        loadLevelButton.onClick.AddListener(OnLoadLevelButton);
        settingsButton.onClick.AddListener(OnSettingsButton);
        closeSettings.onClick.AddListener(OnCloseSettingsButton);
        nicknameInputField.onEndEdit.AddListener(OnNickNameInputField);
    }

    private void OnLoadLevelButton()
    {
        SceneLoader.LoadLevel("Platformer");
    }

    private void OnSettingsButton()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    private void OnCloseSettingsButton()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    
    private void OnNickNameInputField(string nickname)
    {
        Debug.Log($"Nickname {nickname}");
    }
}
