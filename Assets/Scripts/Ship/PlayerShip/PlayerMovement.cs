using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    UIController uIController;
    GameController gameController;
    public GameObject movementTarget;
    public float acceleration = 0.03f;
    public float deceleration = 0.01f;
    [SerializeField]
    float currentSpeed;
    public float baseSpeed = 1f;
    [SerializeField]
    float maxSpeed;
    public float sailLevelSpeedFactor = 1f;
    public int sailLevel = 0;
    int maxSailLevel = 3;
    public float turnSpeed = 0.1f;
    [SerializeField]
    float currentTurnSpeed;
    public CannonManager rightCannons;
    public CannonManager leftCannons;

    void Start(){
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        uIController = GameObject.Find("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            gameController.TogglePlayerPause();
        }
        if (!gameController.paused){
            // Sail level change
            if (Input.GetKeyDown(KeyCode.W)){
                IncreaseSailLevel();
            }
            else if (Input.GetKeyDown(KeyCode.S)){
                DecreaseSailLevel();
            }
            // Turning
            if(Input.GetKey(KeyCode.A)){
                TurnLeft();
            }
            if(Input.GetKey(KeyCode.D)){
                TurnRight();
            }
            // Debug turn-around
            if(Input.GetKey(KeyCode.V)){
                TurnAround();
            }
            if (Input.GetKeyDown(KeyCode.E)){
                rightCannons.FireCannons();
            }
            if (Input.GetKeyDown(KeyCode.Q)){
                leftCannons.FireCannons();
            }
            // Calculate speed forwards
            CalculateSpeed();
            // Move forwards
            MoveForwards();
        }
    }

    public void IncreaseSailLevel(){
        if (sailLevel < maxSailLevel){
            sailLevel++;
            uIController.UpdateSailSpeedDisplay(sailLevel);
            // Calculate current max speed
            maxSpeed = CalculateMaxSpeed();
        }
    }

    public void DecreaseSailLevel(){
        if (sailLevel > 0){
            sailLevel--;
            uIController.UpdateSailSpeedDisplay(sailLevel);
            // Calculate current max speed
            maxSpeed = CalculateMaxSpeed();
        }
    }

    float CalculateMaxSpeed(){
        float sailLevelSpeedImpact = sailLevel * sailLevelSpeedFactor;
        float calculatedMaxSpeed = baseSpeed * sailLevelSpeedImpact;
        return calculatedMaxSpeed;
    }
    void CalculateSpeed(){
        float currentAcceleration = acceleration * sailLevel;
        // Check acceleration if we could be going faster
        if (currentAcceleration > 0){
            if (currentSpeed < maxSpeed){
                currentSpeed = currentSpeed + acceleration;
            }
            else {
                currentSpeed = currentSpeed - deceleration;
            }
        }
        else {
            currentSpeed = currentSpeed - deceleration;
        }
        // Never go backwards
        if (currentSpeed < 0){
            currentSpeed = 0;
        }
    }

    void MoveForwards(){
        Vector3 heading = movementTarget.transform.position - transform.position;
        transform.position += heading * currentSpeed * Time.deltaTime;
    }

    public void TurnLeft(){
        Vector3 newRotation = new Vector3(0 - GetCurrentTurnSpeed(), 0, 0);
        transform.Rotate(newRotation);
    }
    public void TurnRight(){
        Vector3 newRotation = new Vector3(GetCurrentTurnSpeed(), 0, 0);
        transform.Rotate(newRotation);
    }

    public void TurnAround(){
        Vector3 newRotation = new Vector3(180, 0, 0);
        transform.Rotate(newRotation);
        currentSpeed = 0;
    }

    float GetCurrentTurnSpeed(){
        float sailLevelSpeedImpact = maxSailLevel * sailLevelSpeedFactor;
        float calculatedMaxSpeed = baseSpeed * sailLevelSpeedImpact;
        float turnSpeedFactor = calculatedMaxSpeed - (currentSpeed * 0.6f);
        float normalisedTurnSpeedFactor = turnSpeedFactor;
        currentTurnSpeed = turnSpeed * turnSpeedFactor;
        return currentTurnSpeed;
    }
}
