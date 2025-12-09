using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public float startingHealth = 100;
    public Slider healthSlider;
    public static bool isPlayerDead;

    public float deadTime;
    public RespawnManager manager;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;

        isPlayerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            killPlayer();
        }

        Debug.Log("Current health: " + currentHealth);
    }

    // gives the player health (i.e. with potion or powerups)
    public void TakeHealth(float healthAmount)
    {
        if (currentHealth < startingHealth)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, startingHealth);
        }

        Debug.Log("Current health with loot: " + currentHealth);
    }

    // basically the player just 'falls' on its back
    public void killPlayer()
    {
        if(!isPlayerDead) {
            isPlayerDead = true;
            currentHealth = 0;
            
            Debug.Log("Player is dead...");

            transform.Rotate(-90, 0, 0, Space.Self);

            Invoke(nameof(respawnPlayer),deadTime);
        }
    }

    public void respawnPlayer() {
        transform.Rotate(90, 0, 0, Space.Self);
        manager.respawnPlayer();
        isPlayerDead = false;
        currentHealth = startingHealth;
        healthSlider.value = Mathf.Clamp(currentHealth, 0, startingHealth);
    }
}
