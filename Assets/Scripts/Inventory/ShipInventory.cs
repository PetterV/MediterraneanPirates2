using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipInventory : MonoBehaviour
{
    public int unlockedInventorySlots;
    public int ducats;
    public List<InventorySlot> inventory;
    GameController gameController;

    void Start(){
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        SetupShipInventory();
    }

    // Inventory slot handling
    void SetupShipInventory(){
        unlockedInventorySlots = gameController.startingInventorySlots;
        inventory = new List<InventorySlot>();
        AddInventorySlots(unlockedInventorySlots);
        ducats = gameController.startingDucats;
    }

    public void AddInventorySlots(int numberOfSlots){
        int i = 0;
        while (i < numberOfSlots){
            InventorySlot slotToAdd = new InventorySlot();
            inventory.Add(slotToAdd);
            i++;
        }
    }
    public bool CheckForFreeSlot(){
        bool freeSlot = false;
        if(inventory.Exists(x => x.item == null)){
            freeSlot = true;
        }
        return freeSlot;
    }

    // Item Handling
    public void AddItemToInventory(InventoryItem item){
        if (CheckForFreeSlot()){
            InventorySlot slotToFill = inventory.Find(x => x.item == null);
            slotToFill.item = item;
            Debug.Log("Added item: " + item.itemName);
        }
        else {
            Debug.LogError("Trying to add an item when there are no free slots!");
        }
    }

    public void RemoveItemFromInventory(InventoryItem item){
        InventorySlot slotToClear = inventory.Find(x => x.item == item);
        slotToClear.item = null;
    }

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
        }
        else {
            Debug.LogError("Trying to buy something you can't afford!");
        }
    }
    public void GainDucats(int gain){
        ducats = ducats + gain;
    }
}
