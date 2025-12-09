using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenClone : MonoBehaviour
{

    public bool asleep = false;
    public bool canWhirl = false;
    public float dashTime;
    public Animator animator;
    public Arm arm;
    public Player player;
    public float whirlDamage;

    public void dash(Vector3 target) {
        StartCoroutine(doDash(target));
    }

    private IEnumerator doDash(Vector3 target) {
        float time = Time.time;
        Vector3 startPos = this.transform.position;

        while(time + dashTime > Time.time) {
            this.transform.position = Vector3.Lerp(startPos, target, (Time.time - time)/dashTime);
            yield return null;
        }
    }


    public void whirl() {
        animator.SetTrigger("whirl");
        if(canWhirl) {
            player.TakeDamage(whirlDamage);
        }
    }

    public bool shouldWhirl() {
        return canWhirl && !asleep;
    }

    public void toss() {
        this.arm.deploy();
    }

    public void sleep() {
        this.asleep = true;
        animator.SetTrigger("sleep");
    }

    public void wake() {
        this.asleep = false;
        this.animator.SetTrigger("awake");
    }

    public void OnTriggerEnter(Collider c) {
        if(c.gameObject.GetComponentInParent<PlayerController>() != null) {
            canWhirl = true;
        }
    }

    public void OnTriggerExit(Collider c) {
        if(c.gameObject.GetComponentInParent<PlayerController>() != null) {
            canWhirl = false;
        }
    }
}
