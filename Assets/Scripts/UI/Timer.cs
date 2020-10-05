using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Timer : MonoBehaviour {
    Text timerText;

    float timePassed;

    float timeUntilCalamity;

    bool hasTriggeredLastMinute = false;

    public UnityEvent lastMinute;

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
        if(timeLeftInSeconds < 61 && hasTriggeredLastMinute == false){
            hasTriggeredLastMinute = true;
            lastMinute.Invoke();
        }
        string minutes = Mathf.Floor(timeLeftInSeconds/60).ToString("00");
        string seconds = Mathf.Floor(timeLeftInSeconds % 60).ToString("00");
        timerText.text = minutes + " : " + seconds;

    }
}
