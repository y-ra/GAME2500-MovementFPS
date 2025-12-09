using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a tether, which forces the player to remain within some distance of the boss
public class RachelTetherState : RachelBaseState
{

    private float tetherDuration;
    private float tetherAnimationDuration;
    private Tether tether;
    private float percentSwapBombard; //hee hee haw
    private float percentSwapHum;

    public RachelTetherState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer, float tetherDuration, float tetherAnimDuration, Tether tether, float percentSwapBombard, float percentSwapHum) : base(baseContext, factory, speed, rangeAroundPlayer){
        this.tetherDuration = tetherDuration;
        this.tetherAnimationDuration = tetherAnimDuration;
        this.tether = tether;
        this.percentSwapBombard = percentSwapBombard;
        this.percentSwapHum = percentSwapHum;
    }

    public override void enterState(){
        this.baseContext.animator.SetTrigger("tether");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(activateTether, tetherAnimationDuration), tetherAnimationDuration);
    }

    private void activateTether() {
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(deactivateTether, tetherDuration), tetherDuration);
        tether.latch();
        this.checkSwitchStates();
    }

    private void deactivateTether() {
        tether.unlatch();
    }
    

    public override void updateState(){}

    public override void exitState(){}

    public override void checkSwitchStates(){
        float rng = Random.Range(0f,1f);
        if(rng < percentSwapHum) {
            this.switchStates(this.baseFactory.Hum());
        } else if(rng < percentSwapHum + percentSwapBombard) {
            this.switchStates(this.baseFactory.Bombard());
        } else {
            this.switchStates(this.baseFactory.Fly());
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "tether";
    }
}
