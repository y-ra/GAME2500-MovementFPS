using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alex_boss_controller : BossController
{
    public AudioSource audioSource;
    public float punchStrength;
    public punch_arm left;
    public punch_arm right;
    public Rigidbody rigid;

    public override void spawn() {
        base.spawn();

        // BackgroundMusic.bgMusic.Stop();

        audioSource.loop = true;
        audioSource.Play();

        left.punchStrength = punchStrength;
        right.punchStrength = punchStrength;

        state = factory.Roam();
        // rigid.isKinematic = true;
        // state = factory.Juggle();
        //do not uncomment me
        //uncommenting me breaks the animator for reasons (those reasons are that unity is shit)
        // state.enterState();
    }

    public override void takeDamage(float damage) {
        if(this.state.debuginfo() == "block") {
            base.takeDamage(damage/5f);
        } else {
            base.takeDamage(damage);
        }
    }

    
}
