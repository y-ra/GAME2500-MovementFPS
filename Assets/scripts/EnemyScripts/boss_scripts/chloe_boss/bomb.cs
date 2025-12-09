using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public float liveTime;
    public float radius;
    public float damage;
    public bool live = false;
    public Player player;

    public void arm() {
        live = false;
        Invoke(nameof(setArmed),liveTime);
    }

    private void setArmed() {
        Debug.Log("CAN EXPLODE NOW");
        this.live = true;
    }

    public void explode() {
        if(!live) {
            return;
        }

        if((player.transform.position - this.transform.position).magnitude < radius) {

            player.controller.launch((this.transform.position - player.transform.position), 8f);
            player.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision c) {
        explode();
    }
}
