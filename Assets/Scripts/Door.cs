using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable {

    public UnityEvent unityEvent;
    public bool working = true;

    public bool needsKeycard = false;

    public void FixDoor(){
        working = true;
    }

	public override void Interact(){
        if(working){
            if(needsKeycard){
                if(PlayerInventory.instance.HasItem(Item.Keycard)){
                    unityEvent.Invoke();
                }
            }else{
                unityEvent.Invoke();
            }
        }
    }    
}
