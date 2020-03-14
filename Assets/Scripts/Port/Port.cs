using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public int portID;
    public GameObject portScreen;
    public PortScreen portScreenScript;
    public GameObject movementObject;
    GameController gameController;
    public string portName;
    public bool playerInPort;
    public bool showPortScreen;
    public PortInventory portInventory;
    float portDelay = 0.3f;
    float timeRemaining;

    public void SetupPort(){
        Debug.Log("Attempting to set up port " + portName);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        portInventory = GetComponent<PortInventory>();
        portInventory.SetUpPortInventory();
        Debug.Log("Successfully set up " + portName);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == movementObject){
            playerInPort = true;
            timeRemaining = portDelay;
        }
    }

    void Update() {
        if (playerInPort &! showPortScreen){
            timeRemaining = timeRemaining - Time.deltaTime;
            if (timeRemaining <= 0){
                showPortScreen = true;
                ShowPortScreen();
            }
        }
    }

    public void ShowPortScreen(){
        portScreenScript.UpdatePortHeader(portName);
        portScreenScript.currentPort = this;
        portScreenScript.PopulateSellButtonList(portInventory);
        portScreen.SetActive(true);
        gameController.EventPause();
    }

    public void LeavePort(){
        portScreen.SetActive(false);
        showPortScreen = false;
        playerInPort = false;
        movementObject.GetComponent<PlayerMovement>().TurnAround();
        portScreenScript.ClearSellButtons();
        gameController.EventUnpause();
    }
}
