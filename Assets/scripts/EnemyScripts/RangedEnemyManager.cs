using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : MonoBehaviour
{
    public List<RangedEnemy> enemies;
    public Transform player;

    public void notifyEnemies(Vector3 checkPosition) {
        for(int i = 0; i < enemies.Count; i++) {
            enemies[i].checkPosition(checkPosition);
        }
    }
}
