using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public int id;
    ShipInventory shipInventory;
    Image image;
    public bool free = true;

    public InventorySlot(){
        item = null;
    }

    void Awake(){
        image = gameObject.transform.Find("ItemGraphic").GetComponent<Image>();
        shipInventory = GameObject.FindWithTag("Player").GetComponent<ShipInventory>();
    }

    public void AddItem(InventoryItem itemType){
        item = itemType;
        free = false;
        image.sprite = itemType.icon;
        image.color = new Color(0,0,0,1);
    }

    public void RemoveItem(){
        item = null;
        free = true;
        image.sprite = null;
        image.color = new Color(0,0,0,0);
    }

    public void SelectThisSlot(){
        shipInventory.SelectInventorySlot(gameObject);
    }
}
