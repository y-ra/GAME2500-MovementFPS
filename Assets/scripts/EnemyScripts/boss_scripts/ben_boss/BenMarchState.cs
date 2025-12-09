using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenMarchState : BenBaseState
{

    private WhirlCollider whirl;
    private float whirlCooldown;
    private bool canWhirl;
    private float whirlDamage;

    private float swapToEngage;
    private float swapToClone;
    private float swapToPoke;
    private float damageToSmoke;

    private float marchTime;
    private float minMarchTime;
    private float maxMarchTime;
    private bool canEngage;

    public BenMarchState(BossController baseContext, BossFactory factory, float speed, CloneManager manager, WhirlCollider whirl, float whirlCooldown, float whirlDamage, float swapToEngage, float swapToClone, float swapToPoke, float damageToSmoke, float minMarchTime, float maxMarchTime, bool canEngage) : base(baseContext, factory, speed, manager){
        this.whirl = whirl;
        this.whirlCooldown = whirlCooldown;
        this.canWhirl = true;
        this.whirlDamage = whirlDamage;

        this.swapToEngage = swapToEngage;
        this.swapToClone = swapToClone;
        this.swapToPoke = swapToPoke;
        this.damageToSmoke = damageToSmoke;

        this.minMarchTime = minMarchTime;
        this.maxMarchTime = maxMarchTime;
        this.canEngage = canEngage;

        this.marchTime = Random.Range(minMarchTime, maxMarchTime);
    }

    public override void enterState(){
        this.baseContext.animator.SetTrigger("reset");
    }
    

    public override void updateState(){
        this.marchTime -= Time.deltaTime;
        checkSwitchStates();

        if((this.whirl.isColliding && this.canWhirl) || (this.cloneManager.checkCloneWhirl() && this.canWhirl)) {
            this.canWhirl = false;

            this.baseContext.player.TakeDamage(whirlDamage);

            this.baseContext.animator.SetTrigger("whirl");
            this.cloneManager.cloneWhirl();

            this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(resetWhirl, whirlCooldown),whirlCooldown);
        }
    }

    private void resetWhirl() {
        this.baseContext.animator.SetTrigger("reset");
        this.canWhirl = true;
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        if(this.baseContext.damageSinceSwap > damageToSmoke) {
            this.switchStates(this.baseFactory.Smoke());
        } else if(this.marchTime <= 0) {
            randomStateSwap();
        }
    }

    private void randomStateSwap() {
        float rng = Random.Range(0f,1f);

        if((canEngage && this.cloneManager.clones.Count < 3) || rng < swapToEngage) {
            this.switchStates(this.baseFactory.Engage());
        } else if(rng < swapToEngage + swapToPoke) {
            this.switchStates(this.baseFactory.BenPoke());
        } else if(rng < swapToEngage + swapToPoke + swapToClone) {
            this.switchStates(this.baseFactory.Clone());
        } else {
            this.marchTime = Random.Range(minMarchTime, maxMarchTime);
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "march";
    }
}
