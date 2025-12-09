using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public float damagePerSecond;
    public List<DeathBoxCover> cover;
    public Player player;
    public void OnTriggerStay(Collider c) {
        if(c.gameObject.GetComponentInParent<PlayerController>()!=null) {

            for(int i = 0; i < cover.Count; i++) {
                if(cover[i].playerColliding) {
                    return;
                }
            }

            player.TakeDamage(damagePerSecond * Time.deltaTime);
            Debug.Log("DEALING DAMAGE TO PLAYER" + damagePerSecond * Time.deltaTime);
        }
    }
}
