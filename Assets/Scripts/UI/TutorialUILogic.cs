using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUILogic : Singleton<TutorialUILogic> {
    public GameObject toggleUIPrefab;

    public Transform toggleItemParent;

    public Text explanationText;

    public GameObject playerUI;

    public Door cockPitDoor;


    public void EndTutorial(){
        explanationText.gameObject.SetActive(false);
        toggleItemParent.gameObject.SetActive(false);
        playerUI.gameObject.SetActive(true);
        cockPitDoor.working = true;
    }

}
