using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortInventory : MonoBehaviour
{
    Port port;
    InventoryItemController inventoryItemController;
    GameController gameController;
    List<InventoryItem> itemsSoldHere;
    public List<ItemForSale> itemsForSale;
    public void SetUpPortInventory(){
        port = gameObject.GetComponent<Port>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventoryItemController = GameObject.Find("InventoryItemController").GetComponent<InventoryItemController>();
        itemsSoldHere = new List<InventoryItem>();

        Debug.Log(port.portName + " sells:");
        foreach (InventoryItem item in inventoryItemController.itemTypes)
        {
            if (item.ports.Contains(port.portID)){
                itemsSoldHere.Add(item);
                Debug.Log(item.itemName);
            }
        }
        if (itemsSoldHere.Count < 1){
            Debug.LogError("Warning! Found port with no items for sale!");
        }

        SetupItemsForSale();
    }

    void SetupItemsForSale(){
        itemsForSale = new List<ItemForSale>();
        foreach (InventoryItem item in itemsSoldHere)
        {
            ItemForSale newItem = new ItemForSale();
            newItem.item = item;
            newItem.price = CalculatePrice(item.minValue, item.maxValue, item.baseValue);
            itemsForSale.Add(newItem);
        }
    }

    float CalculatePrice(float minPrice, float maxPrice, float baseValue){
        // TODO: Better weight towards baseValue
        float priceTest1 = Random.Range(minPrice, maxPrice);
        float priceTest2 = Random.Range(minPrice, maxPrice);
        // Pick the option closest to the baseValue:
            // Checking the first
        float priceTest1Diff;
        if(priceTest1 < baseValue){
            priceTest1Diff = baseValue - priceTest1;
        }
        else {
            priceTest1Diff = priceTest1 - baseValue;
        }
        // Checking the second
        float priceTest2Diff;
        if(priceTest2 < baseValue){
            priceTest2Diff = baseValue - priceTest2;
        }
        else {
            priceTest2Diff = priceTest2 - baseValue;
        }
        // Evaluating which is closer
        float price;
        if (priceTest1Diff < priceTest2Diff){
            price = priceTest1;
        }
        else {
            price = priceTest2;
        }

        // Return
        return price;
    }
}

public class ItemForSale
{
    public InventoryItem item;
    public float price;
}