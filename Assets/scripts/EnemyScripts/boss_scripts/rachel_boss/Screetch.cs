using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screetch : MonoBehaviour
{

    public int damagePerRing;
    public float speed; 
    public float time;
    public Vector3 targetScale;

    public Player player;
    public Rigidbody rigid;


    void Update()
    {
  
    }

    public void deploy() {
        transform.LookAt(player.transform);
        transform.Rotate(-90,0,0);
        rigid.AddForce((player.transform.position - this.transform.position).normalized * speed, ForceMode.Impulse);
        StartCoroutine(enlarge());
        Invoke(nameof(destroy), time);
    }

    public IEnumerator enlarge() {
        float enterTime = Time.time;
        Vector3 scale = transform.localScale;

        while(enterTime + time > Time.time) {
            transform.localScale = scale + targetScale * ((Time.time - enterTime)/(time));
            yield return null;
        }
    }

    private void destroy() {
        Destroy(gameObject);
    }

    public void OnTriggerStay(Collider c) {
        if(c.gameObject.GetComponentInParent<PlayerController>() == null) {
            return;
        } else {
            Debug.Log("HIT PLAYER >:)");
            this.player.TakeDamage(damagePerRing);
        }

        Destroy(gameObject);
    }
}
