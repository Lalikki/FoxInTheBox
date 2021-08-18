using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button exitGameButton;
    public Button selectLevelButton;
    public string newGameSceneName;
    public GameObject LevelSelectionMenu;
    public void Awake()
    {
        newGameButton.onClick.AddListener(NewGame);
        exitGameButton.onClick.AddListener(ExitGame);
        selectLevelButton.onClick.AddListener(SelectLevel);
    }
    //Select level button
    public void SelectLevel()
    {
        LevelSelectionMenu.SetActive(true);
    }
    //New game button
    public void NewGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    //Exit game button
    public void ExitGame()
    {
        Application.Quit();
        
    }   
}
