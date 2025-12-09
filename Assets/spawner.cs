using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Boss> bosses;

    bool hasSpawned = false;



    void OnTriggerEnter(Collider c) {
        if(c.GetComponentInParent<Player>() != null && !hasSpawned) {
            hasSpawned = true;
            for(int i = 0; i < enemies.Count; i++) {
                enemies[i].SetActive(true);
            }
            for(int i = 0; i < bosses.Count; i++) {
                bosses[i].gameObject.SetActive(true);
                bosses[i].spawn();
            }
        }
    }
}
