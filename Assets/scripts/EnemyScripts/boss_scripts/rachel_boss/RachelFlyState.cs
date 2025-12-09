using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//default roaming state
public class RachelFlyState : RachelBaseState
{

    private float percentSwapTether;
    private float percentSwapHum;
    private float percentSwapBombard;
    private float minFlyTime;
    private float maxFlyTime;
    private float flyTime;
    // private bool enraging = false
    public RachelFlyState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer, float percentSwapTether, float percentSwapHum, float percentSwapBombard, float minFlyTime, float maxFlyTime) : base(baseContext, factory, speed, rangeAroundPlayer){
        this.percentSwapTether = percentSwapTether;
        this.percentSwapHum = percentSwapHum;
        this.percentSwapBombard = percentSwapBombard;
        this.minFlyTime = minFlyTime;
        this.maxFlyTime = maxFlyTime;
        this.flyTime = Random.Range(minFlyTime, maxFlyTime);
    }

    public override void enterState(){
        this.baseContext.animator.SetTrigger("flap");
    }
    

    public override void updateState(){
        checkSwitchStates();
        this.flyTime -= Time.deltaTime;
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        if(this.baseContext.health < this.baseContext.enrageHealthGate) {
            this.switchStates(this.baseFactory.Sing());
        } else if(flyTime <= 0) {
            randomStateSwap();
        }
    }

    private void randomStateSwap() {
        float rng = Random.Range(0f,1f);

        if(rng < percentSwapTether) {
            this.switchStates(this.baseFactory.Tether());
        }else if(rng < percentSwapTether + percentSwapHum) {
            this.switchStates(this.baseFactory.Hum());
        }else if(rng < percentSwapTether + percentSwapHum + percentSwapBombard) {
            this.switchStates(this.baseFactory.Bombard());
        } else {
            this.flyTime = Random.Range(minFlyTime, maxFlyTime);
        }        
    }


    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "fly";
    }
}
