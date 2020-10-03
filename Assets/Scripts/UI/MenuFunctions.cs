using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFunctions : Singleton<MenuFunctions> {
    
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
