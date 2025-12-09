using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch_arm : MonoBehaviour
{
    
    public bool punching;
    public float punchStrength;


    public void setPunchStatus(bool isPunching) {
        this.punching = isPunching;
    }


    public void OnTriggerEnter(Collider other) {
        
        PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();

        if(player != null) {
            Vector3 launchDirection = other.gameObject.transform.position - this.transform.position;
            launchDirection.y = 0;
            player.launch((launchDirection + Vector3.up).normalized, this.punchStrength);
        }
    }
}
