using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public float health;
    public float maxHealth;

    public HitboxManager manager;

    public GameObject canvas;
    public Slider healthSlider;

    public override void takeDamage(float damageAmount)
    {
        if (health > 0)
        {
            health -= damageAmount;
            healthSlider.value = 100 * (health/maxHealth);
        }

        if (health <= 0)
        {
            this.healthSlider.value = 0;
            this.defeat();
        }

        Debug.Log("Current health: " + health);
    }


    public void Start() {
        // this.spawn();
    }

    public virtual void spawn(){
        canvas.SetActive(true);
        this.health = maxHealth;
        healthSlider.value = 100;
    }

    public virtual void defeat(){
        canvas.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
