using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public GameObject playerShip;
    
    public float speed = 2.0f;
    float offsetX;
    float offsetZ;
    
    void Start(){
        offsetX = playerShip.transform.position.x + this.transform.position.x;
        offsetZ = playerShip.transform.position.z + this.transform.position.z;
    }
    void Update () {
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.x = Mathf.Lerp(this.transform.position.x, (playerShip.transform.position.x + offsetX), interpolation);
        position.z = Mathf.Lerp(this.transform.position.z, (playerShip.transform.position.z + offsetZ), interpolation);
        
        this.transform.position = position;
    }
}
