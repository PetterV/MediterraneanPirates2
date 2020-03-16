using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public int unlockedCannons = 3;
    public CannonManager rightCannons;
    public CannonManager leftCannons;

    public void UnlockCannons(int numOfCannons){
        if (unlockedCannons < rightCannons.cannons.Count){
            unlockedCannons = unlockedCannons += numOfCannons;
            rightCannons.SetActiveCannons(unlockedCannons);
            leftCannons.SetActiveCannons(unlockedCannons);
        }
        else {
            Debug.LogError("Tried to increase cannon count beyond what the ship has been set up for!");
        }
    }
}
