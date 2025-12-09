using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fires beam
public class RachelHumState : RachelBaseState
{

    private int ringsToShoot;
    private float ringCooldown;
    private Mouth mouth;

    public RachelHumState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer, int ringsToShoot, float cooldown, Mouth mouth) : base(baseContext, factory, speed, rangeAroundPlayer){
        this.ringsToShoot = ringsToShoot;
        this.ringCooldown = cooldown;
        this.mouth = mouth;
    }

    public override void enterState(){
        this.shootRings();
    }

    private void shootRings() {
        if(ringsToShoot <= 0) {
            return;
        }

        mouth.screetch();
        ringsToShoot--;
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(shootRings, ringCooldown),ringCooldown);
    }
    

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        if(ringsToShoot <= 0) {
            this.switchStates(this.baseFactory.Fly());
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override Vector3 setSpeed(Vector3 desiredVelocity) {
        return Vector3.zero;
    }

    public override string debuginfo() {
        return "hum";
    }
}
