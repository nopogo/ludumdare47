using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {
    Text timerText;

    float timePassed;

    float timeUntilCalamity;

    void Start(){
        timeUntilCalamity = GlobalFunctions.instance.calamityTimerMinutes * 60;
        timerText = GetComponent<Text>();
    }

    void Update(){
        timePassed += Time.deltaTime;
        UpdateTimerText(timeUntilCalamity - timePassed);
    }

    void UpdateTimerText(float timeLeftInSeconds){
        if(timeLeftInSeconds < 0){
            GlobalFunctions.instance.GameOver();
        }
        string minutes = Mathf.Floor(timeLeftInSeconds/60).ToString("00");
        string seconds = Mathf.Floor(timeLeftInSeconds % 60).ToString("00");
        timerText.text = minutes + " : " + seconds;

    }
}
