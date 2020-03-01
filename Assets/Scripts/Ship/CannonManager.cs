using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    float standardShotStrength = 100000f;
    public GameObject cannonball;

    public List<GameObject> cannons;
    float cannonCooldownTime = 0.7f;
    [SerializeField]
    float cannonCooldown;
    public float currentCannonCooldown;
    
    void Update() {
        cannonCooldown = cannonCooldown - Time.deltaTime;
        currentCannonCooldown = cannonCooldown / cannonCooldownTime;
    }

    public void FireCannons(){
        if (cannonCooldown <= 0){
            foreach (GameObject cannon in cannons){
                cannon.GetComponent<Cannon>().Fire(cannonball, standardShotStrength);
            }
            cannonCooldown = cannonCooldownTime;
        }
    }
}
