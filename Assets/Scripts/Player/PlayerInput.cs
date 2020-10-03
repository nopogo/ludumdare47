using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    public GameObject escapeMenu;
    
    void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)){
            Time.timeScale = 0;
            Cursor.visible = true;
            escapeMenu.SetActive(true);
        }
    }
}
