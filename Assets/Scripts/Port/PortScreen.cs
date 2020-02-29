using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class PortScreen : MonoBehaviour
{
    public TextMeshProUGUI portName;
    public Port currentPort;
    ShipInventory shipInventory;
    void Start(){
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
}
