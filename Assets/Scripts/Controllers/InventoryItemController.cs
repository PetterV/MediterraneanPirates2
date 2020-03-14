using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public List <InventoryItem> itemTypes;
    public List<Sprite> imagesSortedByID;
    
    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InventoryItemController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetupInventoryItemController(){
        LoadInventoryItems();
    }

    public void LoadInventoryItems(){
        InventoryList inventoryList = new InventoryList();
        itemTypes = inventoryList.GetInventoryItems();
        foreach (InventoryItem item in itemTypes){
            item.icon = imagesSortedByID[item.iconID];
        }

        Debug.Log("Loaded items: " + itemTypes.Count);
    }
}
