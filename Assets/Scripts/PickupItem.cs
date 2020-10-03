using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable {

    public Item itemType;

	public override void Interact(){
        PlayerInventory.instance.GiveItem(itemType);
        Destroy(gameObject);
    }
}
