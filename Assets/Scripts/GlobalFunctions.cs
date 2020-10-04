using System;
using com.nopogo.Collections;
using UnityEngine;

[Serializable]
public class ItemDictionary : SerializableDictionaryBase<Item, InventoryItem>{}

public class GlobalFunctions : Singleton<GlobalFunctions> {
    public float calamityTimerMinutes = 10f;

    public Machine[] machines;
    public ItemDictionary itemDictionary;


    void Start(){
        machines = FindObjectsOfType<Machine>();
    }

    public void GameOver(){
        MenuFunctions.instance.LoadScene(2);
    }

    public void Victory(){
        MenuFunctions.instance.LoadScene(3);
    }

    public void CheckVictoryState(){
        foreach(Machine machine in machines){
            if(machine.working == false){
                return;
            }
        }
        Victory();
    }


    public void TurnOnGravity(){
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach(Rigidbody rigidbody in rigidbodies){
            PlayerMovement playerMovement = rigidbody.GetComponent<PlayerMovement>();
            if(playerMovement != null){
                playerMovement.EnableGravity();
            }            
            rigidbody.useGravity = true;
        }
    }
}
