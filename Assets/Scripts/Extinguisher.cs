using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : Interactable {

	public override void Interact(){
        FireExtinguisherBehaviour.instance.PickUp(transform);
    }
}
