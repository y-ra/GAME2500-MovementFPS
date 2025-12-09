using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public PlayerController playerMovement;
    public Checkpoint currentRespawn;

    public void newCheckpoint(Checkpoint c) {
        currentRespawn = c;
    }

    public void respawnPlayer() {
        playerMovement.teleport(currentRespawn.getSpawnPosition());
    }
}
