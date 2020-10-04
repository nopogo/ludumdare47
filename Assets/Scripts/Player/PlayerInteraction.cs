﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : Singleton<PlayerInteraction> {

    public Text wailaText;

    public GameObject damageIndication;
    public GameObject healingIndication;

    Camera mainCamera;
    float interactDistance = 5;
    Interactable interactableCache;

    public int health = 100;

    int starthealth;


    public override void Awake(){
        base.Awake();
        starthealth = health;
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
                Outline outlineTemp = interactableCache.GetComponent<Outline>();
                if(outlineTemp != null){
                    outlineTemp.enabled=true;
                }
                return;
            }
        }
        if(interactableCache != null){
            Outline outlineTemp2 = interactableCache.GetComponent<Outline>();
            if(outlineTemp2 != null){
                outlineTemp2.enabled=false;
            }
            interactableCache = null;
        }
    }

    public void GiveDamage(int amount){
        StartCoroutine(FlashDamage());
        health -= amount;
        if(health <=0){
            GlobalFunctions.instance.GameOver();
        }
    }

    IEnumerator FlashDamage(){
        damageIndication.SetActive(true);
        yield return new WaitForSeconds(.1f);
        damageIndication.SetActive(false);
    }

    IEnumerator FlashHealing(){
        healingIndication.SetActive(true);
        yield return new WaitForSeconds(.1f);
        healingIndication.SetActive(false);
    }

    //TODO implement healing
    public void HealPlayer(int amount){
        StartCoroutine(FlashHealing());
        health += amount;
        if(health > starthealth){
            health = starthealth;
        }
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
