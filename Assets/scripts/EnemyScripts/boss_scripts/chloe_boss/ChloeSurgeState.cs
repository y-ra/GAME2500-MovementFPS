using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeSurgeState : ChloeBaseState
{
    private float dashTime;
    private float stunnedTime;
    private float overDashMagnitude;
    private float damage;
    private BossAttackHitbox hitbox;
    private bool damagedPlayer;
    public ChloeSurgeState(BossController baseContext, BossFactory factory, float speed, Turret turret, LayerMask walls, float randomPointDistance, float dashTime, float stunnedTime, float overDashMagnitude, float damage, BossAttackHitbox hitbox) : base(baseContext, factory, speed, turret, walls, randomPointDistance){
        this.dashTime = dashTime;
        this.stunnedTime = stunnedTime;
        this.overDashMagnitude = overDashMagnitude;
        this.damage = damage;
        this.hitbox = hitbox;
    }

    public override void enterState(){
        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(dash));
        damagedPlayer = false;
    }
    
    private IEnumerator dash() {
        float time = Time.time;
        Vector3 flatP = this.baseContext.player.transform.position;
        flatP.y = this.baseContext.transform.position.y;
        Vector3 flatC = this.baseContext.transform.position;
        flatC.y = this.baseContext.transform.position.y;
        Vector3 dest = flatP + (flatP - flatC).normalized * ((flatC - flatP).magnitude + overDashMagnitude);

        while(time + dashTime > Time.time) {
            this.baseContext.transform.position = Vector3.Lerp(flatC, dest, (Time.time - time)/dashTime);
            yield return null;
        }

        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(swap, stunnedTime),stunnedTime);
    }

    private void swap() {
        this.requestSwitchStates(this.baseFactory.Idle());
    }

    public override void updateState(){
        checkSwitchStates();

        if(hitbox.colliding && !damagedPlayer) {
            damagedPlayer = true;
            this.baseContext.player.TakeDamage(damage);
        }
    }

    public override void exitState(){
        //force a new destination point for agent :)
        this.baseContext.agent.destination = this.baseContext.transform.position;
    }

    public override void checkSwitchStates(){
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        this.switchStates(swapTo);
        return true;
    }

    public override string debuginfo() {
        return "Surge";
    }

    public override Vector3 setTarget() {
        return baseContext.player.transform.position;
    }
}
