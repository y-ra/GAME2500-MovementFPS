using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGiveHealth : MonoBehaviour
{
    public float healthAmount = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider c) {
        if(c.gameObject.GetComponentInParent<Player>() == null) {
            return;
        } else {
            Player player = c.gameObject.GetComponentInParent<Player>();
            Debug.Log("HIT PLAYER >:)");
            player.TakeHealth(healthAmount);
        }

        Destroy(gameObject);
    }
}
