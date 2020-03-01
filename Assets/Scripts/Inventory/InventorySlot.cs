using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public int id;
    Image image;
    public bool free = true;

    public InventorySlot(){
        item = null;
    }

    void Awake(){
        image = gameObject.transform.Find("ItemGraphic").GetComponent<Image>();
    }

    public void AddItem(InventoryItem itemType){
        item = itemType;
        free = false;
        image.sprite = itemType.icon;
        image.color = new Color(0,0,0,1);
    }
}
