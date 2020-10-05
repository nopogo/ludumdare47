using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishTick : MonoBehaviour {
    
 

    IEnumerator OnTriggerStay(Collider col){
        if(col.gameObject == null){
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        
        Fire fire = col.gameObject.GetComponent<Fire>();
        if(fire != null){
            fire.RemoveHealth();
        }
    }
}
