using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour { 

    public KeyCode[] keycodes;

    public string explanation;

    Dictionary<KeyCode, Toggle> cachedToggles = new Dictionary<KeyCode, Toggle>();

    public GameObject nextTutorialStage;

    void Start(){
        DeleteOldChildren();
        CreateNewChildren();
    }


    void Update(){
        foreach(KeyCode keycode in keycodes){
            if(Input.GetKeyDown(keycode)){
                cachedToggles[keycode].isOn = true;
                MenuFunctions.instance.CallUIEvent();
                CheckIfStageComplete();
            }
        }
    }

     void DeleteOldChildren(){
        foreach(Transform childTransform in TutorialUILogic.instance.toggleItemParent.GetComponentInChildren<Transform>()){
            if(childTransform != TutorialUILogic.instance.toggleUIPrefab){
                Destroy(childTransform.gameObject);
            }
        }
    }


    void CreateNewChildren(){
        foreach(KeyCode keycode in keycodes){
            TaskRow taskRow = Instantiate(TutorialUILogic.instance.toggleUIPrefab, TutorialUILogic.instance.toggleItemParent).GetComponent<TaskRow>();
            taskRow.SetupTaskRow(keycode);
            cachedToggles[keycode] = taskRow.toggle;
        }
        TutorialUILogic.instance.explanationText.text = explanation;
    }

    void CheckIfStageComplete(){
        foreach(KeyCode key in keycodes){
            if(cachedToggles[key].isOn == false){
                return;
            }
        }
        StartCoroutine(NextStage());
        
        

    }

    IEnumerator NextStage(){
        yield return new WaitForSeconds(1f);
        if(nextTutorialStage != null){
            nextTutorialStage.SetActive(true);
        }else{
            //We have reached the final tutorial stage, end tutorial
           
            TutorialUILogic.instance.EndTutorial();
        }
        gameObject.SetActive(false);
    }


}
