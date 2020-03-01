using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    Transform bottomOfTheOcean;
    float startingHeight;
    float startingDrag;
    float maxDrag;
    void Awake(){
        startingHeight = transform.position.y;
        startingDrag = GetComponent<Rigidbody>().drag;
        maxDrag = startingDrag * 2;
        bottomOfTheOcean = GameObject.Find("BottomOfTheOcean").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        /*float currentDragChange = (transform.position.y - startingHeight) * 0.05f;
        if (currentDragChange < 0){
            GetComponent<Rigidbody>().drag = startingDrag + currentDragChange;
        }*/
        if (transform.position.y < bottomOfTheOcean.position.y){
            Destroy(gameObject);
        }
    }
}
