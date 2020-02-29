using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    public List <InventoryItem> itemTypes;
    
    void Awake(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InventoryItemController");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        LoadActivities();
    }

    public void LoadActivities(){
        InventoryList inventoryList = new InventoryList();
        itemTypes = inventoryList.GetActivities();

        Debug.Log("Loaded items: " + itemTypes.Count);
    }
}
