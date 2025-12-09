using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//yes I did copy and paste the entirety of this from screetch
//however I am also drunk so 🤙🤙🤙🤙🤙🤙🤙🤙🤙🤙🤙🤙🤙🤙
public class ThrowableDisk : MonoBehaviour
{
    public float speed; 
    public float time;
    public Vector3 targetScale;
    public float damage;

    public Player player;
    public Rigidbody rigid;


    void Update()
    {
  
    }

    public void deploy() {
        transform.LookAt(player.transform);
        // transform.Rotate(-90,0,0);
        rigid.AddForce((player.transform.position - this.transform.position).normalized * speed, ForceMode.Impulse);
        Invoke(nameof(destroy), time);
    }



    private void destroy() {
        Destroy(gameObject);
    }

    public void OnTriggerStay(Collider c) {
        if(c.gameObject.GetComponentInParent<PlayerController>() == null) {
            return;
        } else {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
