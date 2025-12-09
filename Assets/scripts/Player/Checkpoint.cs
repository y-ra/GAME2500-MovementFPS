using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;
    public void OnTriggerEnter(Collider c) {
        RespawnManager manager = c.GetComponentInParent<RespawnManager>();
        if(manager != null) {
            manager.newCheckpoint(this);
        }
    }

    public Vector3 getSpawnPosition() {
        return respawnPoint.position;
    }
}
