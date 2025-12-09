using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//non enrage state where boss pepers player with ranged projectiles
public class ABPokeState : BossBaseState
{
    private float startCooldown;
    private int volleys;
    private int shotsPerVolley;
    private float shotCooldown;
    private float volleyCooldown;

    private int shots;
    private float shotTimer;
    private float volleyTimer;

    private MissleLauncher launcher;

    public ABPokeState(BossController baseContext, BossFactory factory, float speed, int volleys, int shotsPerVolley, float shotCooldown, float volleyCooldown, MissleLauncher launcher, float startCooldown) : base(baseContext, factory, speed){
        this.volleys = volleys;
        this.shotsPerVolley = shotsPerVolley;
        this.shotCooldown = shotCooldown;
        this.volleyCooldown = volleyCooldown;
        this.launcher = launcher;
        this.startCooldown = startCooldown;
    }
    public override void enterState(){
        this.shotTimer = 0;
        this.volleyTimer = startCooldown;
        this.baseContext.animator.SetTrigger("poke");
    }
    
    public override void updateState(){
        shotTimer -= Time.deltaTime;
        volleyTimer -= Time.deltaTime;

        checkSwitchStates();
        
        if(volleyTimer < 0) {
            volleys-=1;
            shots = shotsPerVolley;
            volleyTimer = volleyCooldown;
        } else if(shots > 0 && shotTimer < 0) {
            shots -= 1;
            shotTimer = shotCooldown;

            launcher.fire();
        }
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        if(volleys < 0) {
            this.switchStates(this.baseFactory.Roam());
        }
    }

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
        return "poke";
    }
}
