using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlCollider : MonoBehaviour
{
    
    public bool isColliding;

    void OnTriggerEnter(Collider c) {
        if(c.GetComponentInParent<PlayerController>() != null) {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider c) {
        if(c.GetComponentInParent<PlayerController>() != null) {
            isColliding = false;
        }
    }
}
