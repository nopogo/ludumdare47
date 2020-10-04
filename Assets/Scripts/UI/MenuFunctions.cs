using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuFunctions : Singleton<MenuFunctions> {

    public UnityEvent uiEvent;

    public void CallUIEvent(){
        uiEvent.Invoke();
    }
    
    public void LoadScene (int nr){
        Time.timeScale = 1;
        Cursor.visible = true;
        SceneManager.LoadScene(nr);
    }

    public void ResumeScene(){
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
