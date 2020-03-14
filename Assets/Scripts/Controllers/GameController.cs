﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    UIController uIController;
    InventoryItemController inventoryItemController;
    public bool paused = false;
    public bool pausedByPlayer = false;
    public bool pausedByEvent = false;
    public GCData data = new GCData();

    public int startingInventorySlots = 10;
    public int maxInventorySlots = 30;
    public int startingDucats = 300;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        uIController = GameObject.Find("UIController").GetComponent<UIController>();
        inventoryItemController = GameObject.Find("InventoryItemController").GetComponent<InventoryItemController>();
        inventoryItemController.SetupInventoryItemController();
        uIController.SetUpUIController();
        GameObject.FindGameObjectWithTag("Player").GetComponent<ShipInventory>().SetupShipInventory();
        GameObject[] ports = GameObject.FindGameObjectsWithTag("Port");
        foreach (GameObject port in ports)
        {
            Debug.Log("Setting up " + port.name);
            port.GetComponent<Port>().SetupPort();
        }
    }

    public void EventPause(){
        paused = true;
        pausedByEvent = true;
    }
    public void EventUnpause(){
        pausedByEvent = false;
        if (!pausedByPlayer){
            paused = false;
        }
    }
    public void TogglePlayerPause(){
        if (!pausedByPlayer){
            PlayerPause();
        }
        else {
            PlayerUnpause();
        }
    }
    public void PlayerPause(){
        paused = true;
        pausedByPlayer = true;
        uIController.ActivatePauseMenu();
    }

    public void PlayerUnpause(){
        pausedByPlayer = false;
        uIController.DeactivatePauseMenu();
        if (!pausedByEvent){
            paused = false;
        }
    }

    public void LoadGameController(GCData loadedData){
        data = loadedData;
    }
}

public class GCData {

}
