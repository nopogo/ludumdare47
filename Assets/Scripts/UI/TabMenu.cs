using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour {
    public GameObject tabRowPrefab;

    public Transform tabRowParent;


    void OnEnable(){

        DeleteOldChildren();
        CreateNewChildren();
    }


    void DeleteOldChildren(){
        foreach(Transform childTransform in tabRowParent.GetComponentInChildren<Transform>()){
            if(childTransform != tabRowParent){
                if(childTransform.GetComponent<TaskRow>().gravityOne == false){
                    Destroy(childTransform.gameObject);
                }   
            }
        }
    }

    void CreateNewChildren(){
        foreach(Machine machine in GlobalFunctions.instance.machines){
            TaskRow taskRow = Instantiate(tabRowPrefab, tabRowParent).GetComponent<TaskRow>();
            taskRow.SetupTaskRow(machine);
        }
    }


}
