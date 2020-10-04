using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class FloatyThing : MonoBehaviour {

    void Awake(){
        GetComponent<Rigidbody>().AddForce(new Vector3(SmallNr(), SmallNr(),SmallNr()), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(new Vector3(SmallNr(), SmallNr(),SmallNr()), ForceMode.Impulse);
    }

    public float SmallNr (){
        return Random.Range(-.1f, .1f);
    }
}
