﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gyro : MonoBehaviour {
    public Gyro otherGyro;
    
    public bool turnedOn = false;

    public UnityEvent turnOnEvent;
    public UnityEvent turnOffEvent;


	public void Interact(){
        if(GlobalFunctions.instance.reactorWorks && GlobalFunctions.instance.generatorWorks){
            turnedOn = !turnedOn;
            if(turnedOn){
                turnOnEvent.Invoke();
            }else{
                turnOffEvent.Invoke();
            }
            if(otherGyro.turnedOn && turnedOn ==true){
                GlobalFunctions.instance.TurnOnGravity(true);
            }else{
                GlobalFunctions.instance.TurnOnGravity(false);
            }
        }        
    }
}