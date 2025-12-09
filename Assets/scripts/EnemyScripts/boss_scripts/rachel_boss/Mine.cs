using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float mineDamage;
    public float timer;
    void OnTriggerEnter(Collider c) {
        Player p = c.gameObject.GetComponentInParent<Player>();
        if(p != null) {
            //explode
            p.TakeDamage(mineDamage);
            Destroy(gameObject);
        }
        
    }

    void Start() {
        Invoke(nameof(destroySelf),timer);
    }

    private void destroySelf() {
        Destroy(gameObject);
    }
}
