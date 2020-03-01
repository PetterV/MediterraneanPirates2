using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    TextMeshProUGUI sailSpeedDisplay;
    public GameObject pauseMenu;
    public CannonManager leftCannon;
    public Image leftCannonCooldown;
    public CannonManager rightCannon;
    public Image rightCannonCooldown;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("UIController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start(){
        sailSpeedDisplay = GameObject.Find("SailSpeedDisplay").GetComponent<TextMeshProUGUI>();
        UpdateSailSpeedDisplay(0);
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
}
