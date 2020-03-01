using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShipInventory : MonoBehaviour
{
    public int unlockedInventorySlots;
    public int ducats;
    public GameObject itemSlotParent;
    public GameObject itemSlotPrefab;
    public Dictionary<int, GameObject> allInventorySlots;
    GameController gameController;
    int idToSet;

    void Start(){
        allInventorySlots = new Dictionary<int, GameObject>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        SetupShipInventory();
    }

    // Inventory slot handling
    void SetupShipInventory(){
        int idToSet = 1;
        List<GameObject> allFoundInventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot").ToList();
        foreach(GameObject slot in allFoundInventorySlots){
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            slotScript.id = idToSet;
            allInventorySlots.Add(slot.GetComponent<InventorySlot>().id, slot);
            idToSet++;
        }
        ducats = gameController.startingDucats;
    }

    public void AddFreeSlots(int amount){
        int i = 0;
        while (i < amount){
            GameObject newSlot = Instantiate(itemSlotPrefab);
            Image image = newSlot.transform.Find("ItemGraphic").GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            newSlot.transform.SetParent(itemSlotParent.transform);
            InventorySlot newSlotScript = newSlot.GetComponent<InventorySlot>();
            newSlotScript.id = idToSet;
            idToSet++;
            allInventorySlots.Add(newSlot.GetComponent<InventorySlot>().id, newSlot);
            unlockedInventorySlots++;
            i++;
        }
    }
    
    public bool CheckForFreeSlot(){
        bool freeSlot = false;
        foreach(KeyValuePair<int, GameObject> slotEntry in allInventorySlots){
            if (slotEntry.Value.GetComponent<InventorySlot>().free){
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
        }
        else {
            Debug.LogError("Trying to buy something you can't afford!");
        }
    }
    public void GainDucats(int gain){
        ducats = ducats + gain;
    }
}
