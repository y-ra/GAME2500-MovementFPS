using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeBossController : BossController
{
    public LayerMask walls;
    private Vector3 wasLookingAt;
    public AudioSource audioSource;

    public void Start() {
    }
    public override void spawn() {
        base.spawn();
        state = this.factory.Idle();
        this.state.enterState();

        BackgroundMusic.bgMusic.Stop();

        audioSource.loop = true;
        audioSource.Play();

        this.agent.destination = transform.position;
        facePlayer = true;
        wasLookingAt = this.agent.destination;
    }

    protected override void faceDirection() {
        if(wasLookingAt != this.agent.destination) {
            StartCoroutine(smoothLook());

        }
    }

    private IEnumerator smoothLook() {
        Vector3 startLook = wasLookingAt;
        wasLookingAt = this.agent.destination;
        float time = Time.time;

        while(time + 1f > Time.time) {
            this.transform.LookAt(Vector3.Lerp(startLook,this.agent.destination, (Time.time - time)/1f));
            yield return null;
        }
    }
}
