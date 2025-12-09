using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//non enrage state where boss simply moves around
public class ABRoamState : BossBaseState
{
    private float damageToBlock;
    private float chanceSwapCharge;
    private float chanceSwapSlam;
    private float chanceSwapPoke;
    private float minRoamTime;
    private float maxRoamTime;
    private float roamTime;
    private BossAttackHitbox roamBox;
    private bool hitPlayer;
    public ABRoamState(BossController baseContext, BossFactory factory, float speed, float damageToBlock, float chanceSwapCharge, float chanceSwapSlam, float chanceSwapPoke, float minRoamTime, float maxRoamTime, BossAttackHitbox roamBox) : base(baseContext, factory, speed){
        this.damageToBlock = damageToBlock;
        this.chanceSwapCharge = chanceSwapCharge;
        this.chanceSwapSlam = chanceSwapSlam;
        this.chanceSwapPoke = chanceSwapPoke;
        this.minRoamTime = minRoamTime;
        this.maxRoamTime = maxRoamTime;
        this.roamTime = Random.Range(minRoamTime, maxRoamTime);
        this.roamBox = roamBox;
    }
    public override void enterState(){
        baseContext.animator.SetTrigger("roam");
        hitPlayer = false;
    }
    
    public override void updateState(){
        this.roamTime -= Time.deltaTime;
        checkSwitchStates();

        if(roamBox.colliding && !hitPlayer) {
            hitPlayer = true;
            this.baseContext.player.TakeDamage(15f);
            this.baseContext.player.controller.launch((this.baseContext.transform.position - this.baseContext.player.transform.position).normalized, 10);
            this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(resetPunch, .3f),.3f);
        }
    }

    private void resetPunch() {
        this.hitPlayer = false;
    }

    public override void exitState(){}

    public override void checkSwitchStates(){

        // if(Input.GetKey(KeyCode.P)) {
        //     this.switchStates(this.baseFactory.Charge());
        // } else if(Input.GetKey(KeyCode.L)) {
        //     this.switchStates(this.baseFactory.Poke());
        // } else if(Input.GetKey(KeyCode.M)) {
        //     this.switchStates(this.baseFactory.Slam());
        // }
        if(this.baseContext.health < this.baseContext.enrageHealthGate) {
            this.switchStates(this.baseFactory.Juggle());
        }
        else if(this.baseContext.damageSinceSwap > damageToBlock) {
            this.switchStates(this.baseFactory.Block());
        } else if(roamTime <= 0) {
            this.randomStateSwap();
        }
    }

    private void randomStateSwap() {
        float rng = Random.Range(0f,1f);

        if(rng < chanceSwapPoke) {
            this.switchStates(this.baseFactory.Poke());
        } else if(rng < chanceSwapPoke + chanceSwapSlam) {
            this.switchStates(this.baseFactory.Slam());
        } else if(rng < chanceSwapPoke + chanceSwapSlam + chanceSwapCharge) {
            this.switchStates(this.baseFactory.Charge());
        } else {
            this.roamTime = Random.Range(minRoamTime, maxRoamTime);
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "roam";
    }
}
