using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//drops notes from the sky
public class RachelBombardState : RachelBaseState {

    private GameObject notes;
    private GameObject deathBox;
    private float safetyTime;
    private float duration;
    private float entered;

    public RachelBombardState(BossController baseContext, BossFactory factory, float speed, float rangeAroundPlayer, GameObject notes, GameObject deathBox, float stateDuration, float safeTime) : base(baseContext, factory, speed, rangeAroundPlayer){
        this.notes = notes;
        this.deathBox = deathBox;
        this.safetyTime = safeTime;
        this.duration = stateDuration;
    }

    public override void enterState(){
        entered = Time.time;
        notes.SetActive(true);
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(turnOnDeathBox, safetyTime),safetyTime);
    }

    private void turnOnDeathBox() {
        deathBox.SetActive(true);
    }
    

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){
        notes.SetActive(false);
        deathBox.SetActive(false);
    }

    public override void checkSwitchStates(){
        if(entered + duration < Time.time) {
            this.switchStates(this.baseFactory.Fly());
        }
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "bombard";
    }
}
