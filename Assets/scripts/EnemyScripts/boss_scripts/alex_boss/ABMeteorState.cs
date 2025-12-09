using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enrage state where boss disappears and re-appears
public class ABMeteorState : BossBaseState
{
    private BossAttackHitbox punchHitbox;
    private BossAttackHitbox craterHitbox;
    private float duration;
    private float imobileFor;
    private float damage;

    private bool damagedPlayer;
    public ABMeteorState(BossController baseContext, BossFactory factory, float speed, BossAttackHitbox punchHitbox, BossAttackHitbox craterHitbox, float duration, float damage, float imobileFor) : base(baseContext, factory, speed){
        this.punchHitbox = punchHitbox;
        this.craterHitbox = craterHitbox;
        this.duration = duration;
        this.damage = damage;
        this.imobileFor = imobileFor;
    }
    public override void enterState(){
        damagedPlayer = false;
        this.baseContext.animator.SetTrigger("enter-meteor");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(checkSwitchStates, duration),duration);
    }
    
    public override void updateState(){
        if(punchHitbox.colliding && !damagedPlayer) {
            damagedPlayer = true;
            this.baseContext.player.TakeDamage(damage);
            this.baseContext.player.controller.launch((this.baseContext.transform.position - this.baseContext.player.transform.position).normalized, 10);
        } else if(craterHitbox.colliding && !damagedPlayer) {
            damagedPlayer = true;
            this.baseContext.player.TakeDamage(damage/2);
            this.baseContext.player.controller.launch((this.baseContext.transform.position - this.baseContext.player.transform.position).normalized, 10);

        }
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        this.maxSpeed = 0;
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(exit, imobileFor),imobileFor);
    }

    private void exit() {
        this.switchStates(this.baseFactory.Juggle());
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "meteor";
    }
}
