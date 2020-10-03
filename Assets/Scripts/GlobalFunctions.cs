using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunctions : Singleton<GlobalFunctions> {
    public float calamityTimerMinutes = 10f;

    Machine[] machines;

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
}
