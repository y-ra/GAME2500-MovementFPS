using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LineRenderer line;
    public Rigidbody rigid;

    float damage;

    public void Start() {
        line.positionCount = 2;
    }
    public void Deploy(float damage, float time) {
        this.damage = damage;
        Invoke(nameof(DestroyMe), time);

    }

    public void OnTriggerEnter(Collider c) {
        Player p = c.gameObject.GetComponentInParent<Player>();

        if(p != null) {
            Debug.Log("PEW PEW" + damage);
            p.TakeDamage(this.damage);
        } else if(c.gameObject.GetComponentInParent<RangedEnemy>() != null) {
            return;
        }

        DestroyMe();
    }

    public void FixedUpdate() {
        line.SetPosition(0,transform.position - this.rigid.velocity.normalized * 3f);
        line.SetPosition(1,transform.position);
    }

    private void DestroyMe() {
        Destroy(this.gameObject);
    }
}
