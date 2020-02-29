using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortScreen : MonoBehaviour
{
    public TextMeshProUGUI portName;
    public Port currentPort;
    public void UpdatePortHeader(string name){
        portName.text = name;
    }

    public void LeavePort(){
        currentPort.LeavePort();
    }
}
