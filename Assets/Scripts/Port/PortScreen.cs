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
    ShipInventory shipInventory;
    void Start(){
        shipInventory = GameObject.FindWithTag("Player").GetComponent<ShipInventory>();
    }
    public void UpdatePortHeader(string name){
        portName.text = name;
    }

    public void PortSetup(Port port){
        
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
        foreach (ItemForSale item in portInventory.itemsForSale){
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
}
