
using UnityEngine;
using UnityEngine.Events;


public class Machine : Interactable {
    public bool working = false;

    public Item requiredItemToFix;
    public bool consumesItem;

    public string toolTip = "placeholder";

    public UnityEvent fixedEvent;

    public override void Interact(){
        if(working == false && PlayerInventory.instance.HasItem(requiredItemToFix)){
            working = true;
            fixedEvent.Invoke();
            GlobalFunctions.instance.CheckVictoryState();
            if(consumesItem){
                PlayerInventory.instance.RemoveItem(requiredItemToFix);
            }
        }
    }
}
