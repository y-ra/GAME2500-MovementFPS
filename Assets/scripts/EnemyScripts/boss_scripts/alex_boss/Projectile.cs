using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isFired = false;
    public float speed; 

    public PlayerController player;
    public Player playerHealth;
    public float explosionRadius;
    public Rigidbody rigid;


    void Update()
    {
        if(isFired) {
            // rigid.MovePosition(transform.position + Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void deploy() {
        isFired = true;
        rigid.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public virtual void OnCollisionEnter(Collision c) {
        if(c.collider.gameObject.GetComponentInParent<alex_boss_controller>() != null) {
            return;
        }

        if((player.transform.position - this.transform.position).magnitude < explosionRadius) {

            player.launch((this.transform.position - player.transform.position), 10f);
            playerHealth.TakeDamage(5f);
        }

        Destroy(gameObject);
    }
}
