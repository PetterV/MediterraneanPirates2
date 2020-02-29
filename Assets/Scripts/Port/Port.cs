using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public GameObject portScreen;
    public GameObject movementObject;
    GameController gameController;
    public string portName;
    public bool playerInPort;
    public bool showPortScreen;
    float portDelay = 0.3f;
    float timeRemaining;

    void Start(){
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Something entered!");
        if (other.gameObject == movementObject){
            playerInPort = true;
            timeRemaining = portDelay;
        }
        else {
            Debug.Log("Definitely wasn't the player");
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
        portScreen.GetComponent<PortScreen>().UpdatePortHeader(portName);
        portScreen.GetComponent<PortScreen>().currentPort = this;
        portScreen.SetActive(true);
        gameController.EventPause();
    }

    public void LeavePort(){
        portScreen.SetActive(false);
        showPortScreen = false;
        playerInPort = false;
        movementObject.GetComponent<PlayerMovement>().TurnAround();
        gameController.EventUnpause();
    }
}
