using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupItem : Interactable {

    public Item itemType;

    public override void Awake(){
        base.Awake();
        GetComponent<Rigidbody>().AddForce(new Vector3(SmallNr(), SmallNr(),SmallNr()), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(SmallNr(), SmallNr(),SmallNr()), ForceMode.Impulse);
    }

	public override void Interact(){
        PlayerInventory.instance.GiveItem(itemType);
        Destroy(gameObject);
    }

    public float SmallNr (){
        return Random.Range(-.1f, .1f);
    }
}
