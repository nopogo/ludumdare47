using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    public Text wailaText;

    Camera mainCamera;
    float interactDistance = 5;
    Interactable interactableCache;


    void Awake(){
        mainCamera = Camera.main;
    }

    void Update(){
        RayCastForInteractables();
        UpdateWaila();
        if(Input.GetMouseButtonUp(0)){
            Interact();       
        }
    }
    
    void RayCastForInteractables(){
        foreach(RaycastHit hit in GetAllRaycastHits()){
            interactableCache = hit.transform.GetComponent<Interactable>();
            if(interactableCache != null){
                return;
            }
        }
        interactableCache = null;
    }

    void UpdateWaila(){
        string formattedStringName = "";
        if(interactableCache != null){
            formattedStringName = interactableCache.gameObject.name;

            Machine tempMachine = interactableCache.GetComponent<Machine>();
            if(tempMachine != null && tempMachine.working == false){
                formattedStringName += " is broken.";
                
                if(PlayerInventory.instance.HasItem(tempMachine.requiredItemToFix)){
                    formattedStringName += " Fix it";
                }else{
                    formattedStringName += $" You need a {tempMachine.requiredItemToFix} to fix it.";
                }
            }
        }
        wailaText.text = formattedStringName;
    }

    void Interact(){
        if(interactableCache != null){
            interactableCache.Interact();
        }
    }
    
    RaycastHit[] GetAllRaycastHits(){
        RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, interactDistance);
        return hits;
    }
}
