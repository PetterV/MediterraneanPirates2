using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public void Fire(GameObject ball, float shotStrength){
        GameObject cannonBall = Instantiate(ball, transform.position, Quaternion.Euler(0f, 90f, 0f));
        Vector3 shotForce = gameObject.transform.forward * shotStrength;
        cannonBall.GetComponent<Rigidbody>().AddForce(shotForce);
    }
}
