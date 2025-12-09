using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enrage state where player is chased by leaping boss
public class ABJuggleState : BossBaseState
{
    private Rigidbody rigid;
    private float jumpForce;
    private float jumpCooldown;

    private BossAttackHitbox hitbox;
    private bool canHit;
    private float punchCD;
    private float juggleDamage;
    private float juggleKnock;
    private float distanceToMeteor;

    public ABJuggleState(BossController baseContext, BossFactory factory, float speed, Rigidbody rigid, float jumpForce, float jumpCooldown, BossAttackHitbox hitbox, float punchCD, float juggleDamage, float juggleKnock, float distanceToMeteor) : base(baseContext, factory, speed){
        this.rigid = rigid;
        this.jumpForce = jumpForce;
        this.jumpCooldown = jumpCooldown;

        this.hitbox = hitbox;
        this.punchCD = punchCD;
        this.juggleDamage = juggleDamage;
        this.juggleKnock = juggleKnock;
        this.distanceToMeteor = distanceToMeteor;
    }
    public override void enterState(){
        this.baseContext.agent.enabled = false;
        rigid.isKinematic = false;
        canHit = true;
        jumpTowards();
    }
    
    public override void updateState(){
        checkSwitchStates();

        if(hitbox.colliding && canHit) {
            canHit = false;
            this.baseContext.player.TakeDamage(juggleDamage);
            this.baseContext.player.controller.launch((this.baseContext.transform.position - this.baseContext.player.transform.position).normalized, juggleKnock);
            this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(resetPunch, punchCD),punchCD);
        }
    }

    private void resetPunch() {
        canHit = true;
    }

    private void jumpTowards() {

        Vector3 direction = (this.baseContext.player.transform.position - this.baseContext.transform.position).normalized;
        direction = direction * jumpForce;
        direction.y = 8f;
        this.rigid.AddForce(direction, ForceMode.Impulse);

        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(jumpTowards, jumpCooldown),jumpCooldown);
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        //removing this dont want to debug it
        // Debug.Log("DISTANCE:" + (this.baseContext.player.transform.position - this.baseContext.transform.position).magnitude);
        // if((this.baseContext.player.transform.position - this.baseContext.transform.position).magnitude > distanceToMeteor) {
        //     this.switchStates(this.baseFactory.Meteor());
        // }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }


    public override string debuginfo() {
        return "jugge";
    }
}
