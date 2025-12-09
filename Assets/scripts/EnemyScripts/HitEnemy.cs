using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public GameObject enemyDust;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {

            KillEnemy();
        }
    }

    void KillEnemy()
    {
        Instantiate(enemyDust, transform.position, transform.rotation);
        
        gameObject.SetActive(false);

        Destroy(gameObject, 1f);
    }
}
