using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskRow : MonoBehaviour {
    public Toggle toggle;

    public Text labelText;


    void Awake(){
        toggle = GetComponent<Toggle>();
    }


    public void SetupTaskRow(Machine machine){
        labelText.text = machine.name;
        toggle.isOn = machine.working;
    }

    public void SetupTaskRow(KeyCode keycode){
        labelText.text = $"Press: {keycode}";
    }
}
