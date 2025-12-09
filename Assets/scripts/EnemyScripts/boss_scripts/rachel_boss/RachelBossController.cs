using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RachelBossController : BossController
{
    AudioSource audioSource;

    public override void spawn() {
        base.spawn();
        state = this.factory.Fly();
        this.state.enterState();

        BackgroundMusic.bgMusic.Stop();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();

        this.agent.destination = transform.position;
        // state = this.factory.Roam();
    }

    public override void defeat(){
        this.mono.PissBabyInvoke(new InvokeableMethod(nextScene, 3f),3f);
        this.canvas.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void nextScene() {
        SceneManager.LoadScene("BenScene");
    }
}
