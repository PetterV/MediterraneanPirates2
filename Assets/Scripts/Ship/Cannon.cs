using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float cannonCooldownTime = 0.7f;
    [SerializeField]
    float cannonCooldown;
    public float currentCannonCooldown;
    void Update() {
        cannonCooldown = cannonCooldown - Time.deltaTime;
        currentCannonCooldown = cannonCooldown / cannonCooldownTime;
    }
    public void Fire(GameObject ball, float shotStrength){
        if (cannonCooldown <= 0){
            GameObject cannonBall = Instantiate(ball, transform.position, Quaternion.Euler(0f, 90f, 0f));
            Vector3 shotForce = gameObject.transform.forward * shotStrength;
            cannonBall.GetComponent<Rigidbody>().AddForce(shotForce);
            cannonCooldown = cannonCooldownTime;
        }
    }
}
