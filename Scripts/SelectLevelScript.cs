using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelScript : MonoBehaviour
{
    //onClick methods for select level buttons
    public void Level3()
    {
        SceneManager.LoadScene("Level_3");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level_1");
    }   
}
