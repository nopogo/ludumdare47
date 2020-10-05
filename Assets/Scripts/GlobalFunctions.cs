using System;
using com.nopogo.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemDictionary : SerializableDictionaryBase<Item, InventoryItem>{}

public class GlobalFunctions : Singleton<GlobalFunctions> {
    public float calamityTimerMinutes = 10f;

    public Machine[] machines;
    public ItemDictionary itemDictionary;

    public bool reactorWorks = false;
    public bool generatorWorks = false;

    public TaskRow gravityTaskRow;

    public bool gravity = false;

    void Start(){
        machines = FindObjectsOfType<Machine>();
    }

    public void TurnOnReactor(){
        reactorWorks = true;
    }

     public void TurnOnGenerator(){
        generatorWorks = true;
    }

    public void GameOver(){
        MenuFunctions.instance.LoadScene(2);
    }

    public void Victory(){
        MenuFunctions.instance.LoadScene(3);
    }

    public void CheckVictoryState(){
        foreach(Machine machine in machines){
            if(machine.working == false || gravity == false){
                return;
            }
        }
        Victory();
    }


    public void TurnOnGravity(bool turnOn){
        
        if(turnOn){
            Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
            foreach(Rigidbody rigidbody in rigidbodies){
                PlayerMovement playerMovement = rigidbody.GetComponent<PlayerMovement>();
                if(playerMovement != null){
                    playerMovement.EnableGravity(true);
                }            
                rigidbody.useGravity = true;
            }
        }else{
            Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
            foreach(Rigidbody rigidbody in rigidbodies){
                PlayerMovement playerMovement = rigidbody.GetComponent<PlayerMovement>();
                if(playerMovement != null){
                    playerMovement.EnableGravity(false);
                }            
                rigidbody.useGravity = false;
            }
        }
        gravity = turnOn;
        gravityTaskRow.GetComponent<Toggle>().isOn = gravity;
        
    }
}
