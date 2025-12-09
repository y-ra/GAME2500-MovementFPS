using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public bool canGrapple;

    public void OnTriggerEnter(Collider c) {
        if (c.transform.parent != null && c.transform.parent.gameObject.name == "Player") {
            canGrapple = true;
        }
    }

    public void OnTriggerExit(Collider c) {
        if (c.transform.parent != null && c.transform.parent.gameObject.name == "Player") {
            canGrapple = false;
        }
    }
}
