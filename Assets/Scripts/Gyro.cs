using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gyro : Interactable {
    public Gyro otherGyro;
    
    public bool turnedOn = false;

    public UnityEvent turnOnEvent;
    public UnityEvent turnOffEvent;
    float lastTimeInteract = 0f;
    float minWaitTime = 1f;

    public override void Awake(){
        base.Awake();
        foreach(Gyro gyro in  FindObjectsOfType<Gyro>()){
            if(gyro != this){
                otherGyro = gyro;
                return;
            }
        }
    }


	public override void Interact(){
        if(GlobalFunctions.instance.reactorWorks && GlobalFunctions.instance.generatorWorks){
            if((Time.time - lastTimeInteract) < minWaitTime ){
                return;
            }
            lastTimeInteract = Time.time;
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
