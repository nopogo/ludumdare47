using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    [SerializeField]
    int health = 10;
    public int fireDamageAmount = 5;
    public float damageTickDelay = 2f;

    float lastTimeDamaged = 0;

    public void RemoveHealth(){
        health -= 1;
        if(health >= 0){
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider col){
        PlayerInteraction playerInteraction = col.GetComponent<PlayerInteraction>();
        if(playerInteraction != null){
            if( (Time.time - lastTimeDamaged) > damageTickDelay ) {
                playerInteraction.GiveDamage(fireDamageAmount);
                lastTimeDamaged = Time.time;
            }
        }
    }
}
