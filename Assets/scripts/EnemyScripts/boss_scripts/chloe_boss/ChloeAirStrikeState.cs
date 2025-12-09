using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeAirStrikeState : ChloeBaseState
{
    private int artileryShells;
    private int shotsToFire;
    private float timeBetweenFire;
    private float timeBetweenFireAndLand;
    private float timeBetweenLand;
    private GameObject shell;
    private GameObject shellIndicator;

    public ChloeAirStrikeState(BossController baseContext, BossFactory factory, float speed, Turret turret, LayerMask walls, float randomPointDistance, int shots, float timeBetweenFire, float timeBetweenFireAndLand, float timeBetweenLand, GameObject shell, GameObject shellMarker) : base(baseContext, factory, speed, turret, walls, randomPointDistance){
        this.artileryShells = shots;
        this.shotsToFire = shots;
        this.timeBetweenFire = timeBetweenFire;
        this.timeBetweenFireAndLand = timeBetweenFireAndLand;
        this.timeBetweenLand = timeBetweenLand;
        this.shell = shell;
        this.shellIndicator = shellMarker;
    }

    public override void enterState(){
        this.fireShells();
    }

    private void fireShells() {
        Debug.Log("GOING TO FIRE GOING TO FIRE GOING TO FIRE GOING TO FIRE" + shotsToFire);
        if(this.shotsToFire <= 0) {
            this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(dropShells, timeBetweenFireAndLand),timeBetweenFireAndLand);
            return;
        }

        this.shotsToFire--;
        this.turret.animator.SetTrigger("fire-shell");
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(fireShells, timeBetweenFire),timeBetweenFire);
    }

    private void dropShells() {
        
        GameObject marker = this.baseContext.mono.PissBabyInstantiate(shellIndicator, baseContext.player.transform.position, Quaternion.identity);
        GameObject bombb = this.baseContext.mono.PissBabyInstantiate(shell, baseContext.player.transform.position + Vector3.up * 25f, Quaternion.identity);
        bomb bom = bombb.GetComponent<bomb>();
        bom.arm();

        if(artileryShells <= 0) {
            this.checkSwitchStates();
            return;
        }

        this.artileryShells--;
        this.baseContext.mono.PissBabyInvoke(new InvokeableMethod(dropShells, timeBetweenLand),timeBetweenLand);
    }
    

    public override void updateState(){
    }

    public override void exitState(){}

    public override void checkSwitchStates(){
        this.switchStates(this.baseFactory.Idle());
    }

    public override bool requestSwitchStates(BossBaseState swapTo){
        return false;
    }

    public override string debuginfo() {
        return "AirStrike";
    }
}
