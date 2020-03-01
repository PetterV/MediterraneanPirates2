using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    float standardShotStrength = 100000f;
    public GameObject cannonball;

    public List<GameObject> cannons;

    public void FireCannons(){
        foreach (GameObject cannon in cannons){
            cannon.GetComponent<Cannon>().Fire(cannonball, standardShotStrength);
        }
    }
}
