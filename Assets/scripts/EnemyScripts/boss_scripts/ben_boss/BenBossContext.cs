using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenBossContext : BossController
{
    public AudioSource audioSource;

    public override void spawn() {
        base.spawn();
        state = factory.March(true);
        this.state.enterState();

        BackgroundMusic.bgMusic.Stop();
        audioSource.loop = true;
        audioSource.Play();

        this.agent.destination = transform.position;
        facePlayer = true;
    }
}

