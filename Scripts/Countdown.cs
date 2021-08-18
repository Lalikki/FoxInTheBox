using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public float timeStart = 0;
    public Text timeTextBox;
    //Set start time to 0
    void Start()
    {
        timeTextBox.text = timeStart.ToString();
    }
    //Incease time by deltatime and convert to string for textbox
    void Update()
    {
        timeStart += Time.deltaTime;
        int seconds = (int)(timeStart % 60);
        int minutes = (int)(timeStart / 60) % 60;
        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timeTextBox.text = timerString;
    }
}
