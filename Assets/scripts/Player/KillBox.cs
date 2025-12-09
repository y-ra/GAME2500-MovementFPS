using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    void OnTriggerEnter(Collider c) {
        Player player = c.GetComponentInParent<Player>();
        if(player != null) {
            player.killPlayer();
        }
    }
}
