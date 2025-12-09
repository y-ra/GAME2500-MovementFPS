using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//dash to target, spawn shadow at start position
public class BenEngageState : BenBaseState
{
    private Vector3 startPos;
    private float overDashMagnitude;
    private float dashTime;
    private WhirlCollider hitbox;
    private float dashDamage;
    private bool hasCollided;

    public BenEngageState(BossController baseContext, BossFactory factory, float speed, CloneManager manager, float overDashMagnitude, float dashTime, WhirlCollider hitbox, float damage) : base(baseContext, factory, speed, manager){
        this.overDashMagnitude = overDashMagnitude;
        this.dashTime = dashTime;
        this.hitbox = hitbox;
        hasCollided = false;
    }

    public override void enterState(){
        this.baseContext.animator.SetTrigger("dash");
        startPos = this.baseContext.transform.position;

        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(dash));
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

        this.requestSwitchStates(this.baseFactory.March(false));
    }
    

    public override void updateState(){

        if(this.hitbox.isColliding && !hasCollided) {
            hasCollided = true;
            this.baseContext.player.TakeDamage(dashDamage);
        }
    }

    public override void exitState(){
        //spawn clone at start position
        this.cloneManager.spawnClone(startPos);
    }

    public override void checkSwitchStates(){
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        this.switchStates(swapTo);
        return true;
    }

    public override string debuginfo() {
        return "engage";
    }
}
