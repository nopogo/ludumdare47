using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireExtinguisherBehaviour : Singleton<FireExtinguisherBehaviour> {
    public Transform parentTransform;
    public Transform forcePosition;
    public Text toolTipText;
    public float shootForce = 10f;

    bool holdingExtinguisher = false;
    Transform extinguisherCache;
    Rigidbody playerRigidbody;
    BoxCollider shootCollider;


    public override void Awake(){
        base.Awake();
        playerRigidbody = GetComponent<Rigidbody>();
        shootCollider = parentTransform.GetComponent<BoxCollider>();
        shootCollider.enabled = false;
    }

    void Update(){
        if(holdingExtinguisher ){
            if(Input.GetMouseButton(0)){
                shootCollider.enabled = true;
                ExtinguisherForce();
                AudioManager.instance.PlayExtinguisher(true);
            }if(Input.GetMouseButtonUp(0)){
                shootCollider.enabled = false;
                AudioManager.instance.PlayExtinguisher(false);
            }
            if(Input.GetMouseButtonUp(1)){
                Drop();
            }
        }
    }

    public void PickUp(Transform pickupTransform){
        ToggleHolding(pickupTransform);
    }

    public void Drop(){
        ToggleHolding();
    }

    void ToggleHolding(Transform pickupTransform=null){
        if(pickupTransform != null){
            extinguisherCache = pickupTransform;
        }
        holdingExtinguisher = !holdingExtinguisher;
        if(holdingExtinguisher){
            extinguisherCache.SetParent(parentTransform);
            extinguisherCache.position = parentTransform.position;
            extinguisherCache.rotation = parentTransform.rotation;
        }else{
            extinguisherCache.SetParent(null);
        }
        
        extinguisherCache.GetComponent<BoxCollider>().enabled = !holdingExtinguisher;
        extinguisherCache.GetComponent<Rigidbody>().isKinematic=holdingExtinguisher;
        toolTipText.gameObject.SetActive(holdingExtinguisher);
    }

    void ExtinguisherForce(){
        playerRigidbody.AddForceAtPosition(-forcePosition.forward * shootForce, forcePosition.position, ForceMode.Impulse);
    }
}
