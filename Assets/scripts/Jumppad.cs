using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    public float launchForce;

    void OnTriggerEnter(Collider c) {
        Player p = c.gameObject.GetComponentInParent<Player>();
        if(c != null) {
            p.controller.launch(Vector3.up, launchForce);
        }
    }
}
