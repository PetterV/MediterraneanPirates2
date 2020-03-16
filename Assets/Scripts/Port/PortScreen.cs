using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PortScreen : MonoBehaviour
{
    public TextMeshProUGUI portName;
    public Port currentPort;
    PortInventory portInventory;
    public GameObject sellButtonsPrefab;
    public List<GameObject> sellButtons;
    public GameObject playerSellButton;
    ShipInventory shipInventory;
    public TextMeshProUGUI itemBeingSoldName;
    public TextMeshProUGUI itemBeingSoldCost;
    public void SetupPortScreen(){
        shipInventory = GameObject.FindWithTag("Player").GetComponent<ShipInventory>();
    }
    public void UpdatePortHeader(string name){
        portName.text = name;
    }

    public void LeavePort(){
        currentPort.LeavePort();
    }

    public void DebugBuyMarble(){
        InventoryItem marbleToSell = GameObject.Find("InventoryItemController").GetComponent<InventoryItemController>().itemTypes.Find(x => x.itemName == "Marble");
        shipInventory.PayDucats(Mathf.RoundToInt(marbleToSell.baseValue));
        shipInventory.AddItemToInventory(marbleToSell);
    }

    public void PopulateSellButtonList(PortInventory portInventory){
        sellButtons = new List<GameObject>();
        foreach (ItemForSale item in portInventory.itemsPurchasableHere){
            GameObject newButton = Instantiate(sellButtonsPrefab);
            newButton.transform.SetParent(sellButtonsPrefab.transform.parent);
            newButton.transform.localScale = sellButtonsPrefab.transform.localScale;
            SaleButton buttonScript = newButton.GetComponent<SaleButton>();
            buttonScript.itemForSale = item;
            buttonScript.itemName.text = item.item.itemName;
            buttonScript.itemCost.text = item.price.ToString();

            sellButtons.Add(newButton);

            newButton.SetActive(true);
        }
    }

    public void ClearSellButtons(){
        foreach (GameObject button in sellButtons)
        {
            Destroy(button);
        }
    }

    public void UpdateItemSaleView(){
        if (shipInventory.selectedSlot != null && shipInventory.selectedSlot.GetComponent<InventorySlot>().item != null){
            ItemForSale itemForSale = currentPort.GetComponent<PortInventory>().itemsSellPricesHere.Find(x => x.item == shipInventory.selectedSlot.GetComponent<InventorySlot>().item);

            itemBeingSoldName.text = itemForSale.item.itemName;
            itemBeingSoldCost.text = itemForSale.price.ToString();
            playerSellButton.SetActive(true);
        }
        else {
            itemBeingSoldName.text = "No item selected";
            itemBeingSoldCost.text = "";
            playerSellButton.SetActive(false);
        }
    }

    public void PlayerSellItem(){
        ItemForSale itemForSale = currentPort.GetComponent<PortInventory>().itemsSellPricesHere.Find(x => x.item == shipInventory.selectedSlot.GetComponent<InventorySlot>().item);
        if (itemForSale == null){
            Debug.LogError("ItemforSale not found!");
        }
        shipInventory.GainDucats((int)itemForSale.price);
        shipInventory.selectedSlot.GetComponent<InventorySlot>().RemoveItem();

        UpdateItemSaleView();
    }
}
