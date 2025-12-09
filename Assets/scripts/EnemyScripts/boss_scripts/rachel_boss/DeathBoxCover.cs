using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxCover : MonoBehaviour
{
    public bool playerColliding;
    public void OnTriggerEnter(Collider c) {
        if(c.gameObject.GetComponentInParent<Player>()!=null) {
            playerColliding = true;
        }
    }

    public void OnTriggerExit(Collider c) {
        if(c.gameObject.GetComponentInParent<Player>()!=null) {
            playerColliding = false;
        }
    }
}
