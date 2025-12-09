using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellBehavior : MonoBehaviour
{
    Player playerHealth;
    public int damage = 5;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Player>();
        
        transform.LookAt(player.transform);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
