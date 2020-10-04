using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Interactable {

	public override void Interact(){
        PlayerInteraction.instance.HealPlayer(20);
        Destroy(gameObject);
    }
}
