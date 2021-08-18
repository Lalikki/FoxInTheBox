using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Countdown2 : MonoBehaviour
{
    public float timeStart = 0;
    public Text timeTextBox;
    public GameObject gameOverMenuUI;
    public AudioSource gametheme;
    //Set start time to 4 minutes 0 seconds
    void Start()
    {
        timeTextBox.text = timeStart.ToString();
    }
    //Countdown time for level 2
    void Update()
    {
        timeStart -= Time.deltaTime;

        int seconds = (int)(timeStart % 60);
        int minutes = (int)(timeStart / 60) % 60;
        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeTextBox.text = timerString;
        if (timeStart < 0)
        {
            gametheme.Pause();
            Time.timeScale = 0f;
            gameOverMenuUI.SetActive(true);
        }
    }
}
