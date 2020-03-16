using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PortInventory : MonoBehaviour
{
    Port port;
    InventoryItemController inventoryItemController;
    GameController gameController;
    List<InventoryItem> inventoryItemsPurchasableHere;
    public List<ItemForSale> itemsPurchasableHere;
    public List<ItemForSale> itemsSellPricesHere;
    float itemForPurchaseSellPriceFactor = 0.7f:
    public void SetUpPortInventory(){
        port = gameObject.GetComponent<Port>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventoryItemController = GameObject.Find("InventoryItemController").GetComponent<InventoryItemController>();
        inventoryItemsPurchasableHere = new List<InventoryItem>();

        foreach (InventoryItem item in inventoryItemController.itemTypes)
        {
            if (item.ports.Contains(port.portID)){
                inventoryItemsPurchasableHere.Add(item);
                Debug.Log(item.itemName);
            }
        }
        if (inventoryItemsPurchasableHere.Count < 1){
            Debug.LogError("Warning! Found port with no items for sale!");
        }

        SetupItemsForPurchase();
        SetUpBuyList();
    }

    void SetupItemsForPurchase(){
        itemsPurchasableHere = new List<ItemForSale>();
        foreach (InventoryItem item in inventoryItemsPurchasableHere)
        {
            ItemForSale newItem = new ItemForSale();
            newItem.item = item;
            newItem.price = CalculatePurchasePrice(item.minValue, item.maxValue, item.baseValue);
            itemsPurchasableHere.Add(newItem);
        }
    }

    float CalculatePurchasePrice(float minPrice, float maxPrice, float baseValue){
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

        price = Mathf.Round(price);

        // Return
        return price;
    }

    float CalculateSellPrice(ItemForSale item){
        float price;
        ItemForSale existingItem = itemsPurchasableHere.Find(x => x.item.id == item.item.id);
        if (existingItem != null){
            price = existingItem.price * itemForPurchaseSellPriceFactor;
        }
        else {
            float priceTest1 = Random.Range(item.item.minValue, item.item.maxValue);
            float priceTest2 = Random.Range(item.item.minValue, item.item.maxValue);
            if (priceTest1 > priceTest2){
                price = priceTest1;
            }
            else {
                price = priceTest2;
            }
        }

        price = Mathf.Round(price);

        return price;
    }

    void SetUpBuyList(){
        itemsSellPricesHere = new List<ItemForSale>();

        foreach(InventoryItem item in inventoryItemController.itemTypes){
            ItemForSale newItem = new ItemForSale();
            newItem.item = item;
            newItem.price = CalculateSellPrice(newItem);

            itemsSellPricesHere.Add(newItem);
        }
    }
}

public class ItemForSale
{
    public InventoryItem item;
    public float price;
}