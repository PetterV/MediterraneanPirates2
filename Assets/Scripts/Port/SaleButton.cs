using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaleButton : MonoBehaviour
{
    public ItemForSale itemForSale;
    public ShipInventory playerInventory;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCost;

    void Awake(){
        playerInventory = GameObject.FindWithTag("Player").GetComponent<ShipInventory>();
    }
    public void SellItemToPlayer(){
        if (playerInventory.CheckAffordability((int)itemForSale.price)){
            playerInventory.AddItemToInventory(itemForSale.item);
            playerInventory.PayDucats((int) itemForSale.price);
        }
    }
}
