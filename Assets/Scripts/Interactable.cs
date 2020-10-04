using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour {


    public virtual void Awake(){
        Outline outline = GetComponent<Outline>();
        if(outline != null){
            outline.enabled = false;
        }
    }


    public virtual void Interact(){
    }
}
