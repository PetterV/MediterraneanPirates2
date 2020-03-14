using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShipInventory : MonoBehaviour
{
    public int ducats;
    public int startingSlots = 10;
    int unlockedSlots;
    public GameObject itemSlotParent;
    public GameObject itemSlotPrefab;
    public Dictionary<int, GameObject> allInventorySlots = new Dictionary<int, GameObject>();
    GameController gameController;
    UIController uIController;
    public int idToSet;

    // Inventory slot handling
    public void SetupShipInventory(){
        // Fetch controllers
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        uIController = GameObject.Find("UIController").GetComponent<UIController>();
        // Start at 1 for convenience
        idToSet = 1;
        // Find all the possible inventoryslots
        List<GameObject> allFoundInventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot").ToList();
        foreach(GameObject slot in allFoundInventorySlots){
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            slotScript.id = idToSet; // Set the internal ID
            idToSet++; // Iterate ID for the next entry's sake
            allInventorySlots.Add(slot.GetComponent<InventorySlot>().id, slot); // Add it to the database
            // Deactive slots that start locked
            if (slotScript.id > startingSlots){
                slotScript.free = false;
                slot.SetActive(false);
            }
        }
        unlockedSlots = startingSlots; // Set the number of unlocked slots, for the sake of later unlocks
        
        // Give player their starting money
        ducats = gameController.startingDucats;
        uIController.UpdateDucatCount(ducats);
    }

    public void AddFreeSlots(int amount){ // Method to unlock more slots once the game has started
        int idToUnlock = unlockedSlots + 1; // Get the lowest ID you'll be unlocking
        int idEnd = unlockedSlots + amount; // Get the highest ID you'll be unlocking
        // Unlock all relevant entries:
        while (idToUnlock <= idEnd){
            allInventorySlots[idToUnlock].GetComponent<InventorySlot>().free = true;
            allInventorySlots[idToUnlock].SetActive(true);
            idToUnlock++;
        }
        unlockedSlots = idEnd; // Set the new amount of unlocked slots
        Debug.Log("The size of the inventory is now " + allInventorySlots.Count);
    }
    
    public bool CheckForFreeSlot(){
        bool freeSlot = false;
        foreach(KeyValuePair<int, GameObject> slotEntry in allInventorySlots){
            if (!slotEntry.Value.activeSelf){
                return freeSlot;
            }
            else if (slotEntry.Value.GetComponent<InventorySlot>().free){
                Debug.Log("Slot " + slotEntry.Value.GetComponent<InventorySlot>().id + " is free");
                freeSlot = true;
                return freeSlot;
            }
        }
        return freeSlot;
    }
    public int FindNextFreeSlotID(){
        int nextFreeSlot = allInventorySlots.First(s => s.Value.GetComponent<InventorySlot>().free).Value.GetComponent<InventorySlot>().id;
        return nextFreeSlot;
    }

    // Item Handling
    public void AddItemToInventory(InventoryItem item){
        if (CheckForFreeSlot()){
            int slotID = FindNextFreeSlotID();
            InventorySlot slotToFill = allInventorySlots[slotID].GetComponent<InventorySlot>();
            slotToFill.AddItem(item);
            Debug.Log("Added item: " + item.itemName);
        }
        else {
            Debug.LogError("Trying to add an item when there are no free slots!");
        }
    }

    /*public void RemoveItemFromInventory(InventoryItem item){
        InventorySlot slotToClear = allInventorySlots.Find(x => x.GetComponent<InventorySlot>().item == item).GetComponent<InventorySlot>();
        slotToClear.item = null;
    }*/

    // Money handling
    public bool CheckAffordability(int cost){
        bool canAfford = false;
        if (cost < ducats){
            canAfford = true;
        }
        return canAfford;
    }
    public void PayDucats(int cost){
        if (CheckAffordability(cost)){
            ducats = ducats - cost;
            uIController.UpdateDucatCount(ducats);
        }
        else {
            Debug.LogError("Trying to buy something you can't afford!");
        }
    }
    public void GainDucats(int gain){
        ducats = ducats + gain;
        uIController.UpdateDucatCount(ducats);
    }
}
