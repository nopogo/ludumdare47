using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable {

    public UnityEvent unityEvent;
    public bool working = true;

	public override void Interact(){
        if(working){
            unityEvent.Invoke();
        }
        
    }    
}
