using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//non enraged state where boss jumps and then smashes down
public class ABSlamState : BossBaseState
{
    private float bounceTime;//on the ground bending knees
    private float jumpTime;//in the air
    private float windTime;//floating in the air
    private float slamTime;//puching down onto the ground

    private float slamDamage;
    private float slamKnock;

    private BossAttackHitbox slamHitbox;
    private bool hasDamaged;
    private Transform punchPoint; //will try to get this into the player



    public ABSlamState(BossController baseContext, BossFactory factory, float speed, float bounceTime, float windTime, float jumpTime, float slamTime, Transform punchPoint, BossAttackHitbox hitbox, float slamDamage, float slamKnock) : base(baseContext, factory, speed){
        this.bounceTime = bounceTime;
        this.windTime = windTime;
        this.jumpTime = jumpTime;
        this.slamTime = slamTime;
        this.punchPoint = punchPoint;
        this.hasDamaged = false;
        this.slamHitbox = hitbox;
        this.slamDamage = slamDamage;
        this.slamKnock = slamKnock;
    }
    public override void enterState(){
        this.baseContext.agent.enabled = false;
        swapBounce();
    }
    
    public override void updateState(){
        if(slamHitbox.colliding && !hasDamaged) {
            hasDamaged = true;
            this.baseContext.player.TakeDamage(slamDamage);
            this.baseContext.player.controller.launch((this.punchPoint.position - this.baseContext.player.transform.position).normalized, slamKnock);
        }
    }

    private void swapBounce() {
        this.baseContext.animator.SetTrigger("slam-jump");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(swapJump, bounceTime),bounceTime);
    }

    private void swapJump() {
        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(jump));
    }

    private IEnumerator jump() {
        Vector3 target = baseContext.transform.position + Vector3.up*10f;
        Vector3 start = baseContext.transform.position;
        float time = Time.time;

        while(time + jumpTime > Time.time) {
            baseContext.transform.position = Vector3.Lerp(start, target, (Time.time-time)/jumpTime);
            yield return null;
        }

        this.swapFloat();
    }

    private void swapFloat() {
        this.baseContext.animator.SetTrigger("slam-wind");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(swapSlam, windTime),windTime);
    }
    private void swapSlam() {
        
        this.baseContext.animator.SetTrigger("slam-punch");
        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(slam));
    }

    private IEnumerator slam() {
        Vector3 dif = this.baseContext.transform.position - punchPoint.position;
        Vector3 target = this.baseContext.player.transform.position + dif;
        Vector3 start = baseContext.transform.position;
        float time = Time.time;

        while(time + slamTime > Time.time) {
            baseContext.transform.position = Vector3.Lerp(start, target, (Time.time-time)/slamTime);
            yield return null;
        }
        this.switchStates(this.baseFactory.Roam());
    }

    public override void exitState(){
        this.baseContext.agent.enabled = true;
    }

    public override void checkSwitchStates(){}

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "slam";
    }
}
