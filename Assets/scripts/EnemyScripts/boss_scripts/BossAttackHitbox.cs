using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{
    public bool colliding;


    public void OnTriggerEnter(Collider c) {
        if(c.GetComponentInParent<Player>() != null) {
            colliding = true;
        }
    }


    public void OnTriggerExit(Collider c) {
        if(c.GetComponentInParent<Player>() != null) {
            colliding = false;
        }
    }
}
