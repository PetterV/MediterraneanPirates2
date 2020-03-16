using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    TextMeshProUGUI sailSpeedDisplay;
    TextMeshProUGUI ducatCount;
    public GameObject pauseMenu;
    public CannonManager leftCannon;
    public Image leftCannonCooldown;
    public CannonManager rightCannon;
    public Image rightCannonCooldown;
    public PortScreen portScreen;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UIController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetUpUIController(){
        sailSpeedDisplay = GameObject.Find("SailSpeedDisplay").GetComponent<TextMeshProUGUI>();
        ducatCount = GameObject.Find("DucatCounter").GetComponent<TextMeshProUGUI>();
        UpdateSailSpeedDisplay(0);
        portScreen.SetupPortScreen();
    }

    public void UpdateSailSpeedDisplay(int value){
        sailSpeedDisplay.text = value.ToString();
    }

    public void ActivatePauseMenu(){
        pauseMenu.SetActive(true);
    }
    public void DeactivatePauseMenu(){
        pauseMenu.SetActive(false);
    }
    public void UpdateCannonCooldown(){
        float leftFill = 1f - leftCannon.currentCannonCooldown;
        if (leftFill > 1f){
            leftFill = 1f;
        }
        leftCannonCooldown.fillAmount = leftFill;

        float rightFill = 1f - rightCannon.currentCannonCooldown;
        if (rightFill > 1f){
            rightFill = 1f;
        }
        rightCannonCooldown.fillAmount = rightFill;
    }

    private void Update() {
        UpdateCannonCooldown();
    }

    public void UpdateDucatCount(int ducats){
        ducatCount.text = ducats.ToString();
    }
}
