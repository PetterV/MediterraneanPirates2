using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public float shipHealth = 1000f;

    void OnCollisionEnter(Collision other) {
        Debug.Log("Deceted collision from " + other.gameObject.name);
        if(other.gameObject.tag == "AttackObject"){
            TakeDamage(other.gameObject.GetComponent<DamageDealing>().damage);
            if (other.gameObject.GetComponent<DamageDealing>().destroyOnHit){
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(float damage){
        shipHealth = shipHealth - damage;
        if(CheckForSinking()){
            StartSinking();
        }
    }

    public bool CheckForSinking(){
        bool sinking = false;
        if (shipHealth <= 0){
            sinking = true;
        }
        return sinking;
    }

    public void StartSinking(){
        Debug.Log("Sunk " + gameObject.name + "!");
        GetComponent<BoxCollider>().isTrigger = true;
        Destroy(gameObject, 10);
    }
}
