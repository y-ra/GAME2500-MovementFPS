using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//non enrage state where boss charges up a punch :)
public class ABChargeState : BossBaseState
{

    private float windupTime;
    private float aimPunchDelay;
    private float punchDuration;
    private float punchForce;
    private float punchDamage;
    private float overDashMagnitude;
    private Vector3 target;
    private BossAttackHitbox punchHitbox;
    private bool hitPlayer;


    public ABChargeState(BossController baseContext, BossFactory factory, float speed, float windupTime, float aimPunchDelay, float punchDuration, float punchForce, float punchDamage, float overDashMagnitude, BossAttackHitbox punchHitbox) : base(baseContext, factory, speed){
        this.windupTime = windupTime;
        this.aimPunchDelay = aimPunchDelay;
        this.punchDuration = punchDuration;
        this.punchForce = punchForce;
        this.punchDamage = punchDamage;
        this.overDashMagnitude = overDashMagnitude;
        this.punchHitbox = punchHitbox;
    }
    
    public override void enterState(){
        this.baseContext.animator.SetTrigger("charge");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(windup, windupTime),windupTime);
    }
    
    private void windup() {
        target = this.baseContext.player.transform.position;
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(startPunch, aimPunchDelay),aimPunchDelay);
    }

    private void startPunch() {
        this.baseContext.mono.PissBabyCoroutine(new CoroutineableMethod(punch));
    }

    public override void updateState(){
        if(punchHitbox.colliding && !hitPlayer) {
            hitPlayer = true;
            this.baseContext.player.TakeDamage(punchDamage);
            this.baseContext.player.controller.launch((this.baseContext.transform.position - this.baseContext.player.transform.position).normalized, punchForce);
        }
    }

    public override void exitState() {
        baseContext.facePlayer = true;
    }

    public override void checkSwitchStates(){}

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override Vector3 setTarget() {
        return baseContext.player.transform.position;
    }

    public override Vector3 setSpeed(Vector3 desiredVelocity) {
        return Vector3.zero;
    }

    public override string debuginfo() {
        return "charge";
    }

    private IEnumerator punch() {
        float time = Time.time;
        Vector3 flatP = this.target;
        flatP.y = this.baseContext.transform.position.y;
        Vector3 flatC = this.baseContext.transform.position;
        flatC.y = this.baseContext.transform.position.y;
        Vector3 dest = flatP + (flatP - flatC).normalized * ((flatC - flatP).magnitude + overDashMagnitude);

        while(time + punchDuration > Time.time) {
            this.baseContext.transform.position = Vector3.Lerp(flatC, dest, (Time.time - time)/punchDuration);
            yield return null;
        }

        this.switchStates(this.baseFactory.Roam());
    }
}
