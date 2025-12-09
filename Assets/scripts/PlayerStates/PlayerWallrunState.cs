using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallrunState : PlayerBaseState
{
    private float jumpForce;

    public PlayerWallrunState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed, float jumpForce)  : base (context, factory, speed, rigid){
        this.jumpForce = jumpForce;
    }

    public override void enterState(){
        this.rigid.useGravity = false;
    }

    public override void updateState(){
        checkSwitchStates();
        checkDismount();
    }

    public override void exitState(){
        this.rigid.useGravity = true;
    }

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        return false;
    }

    public override void jump() {}

    private void checkDismount() {
        //this lets u climb up the wall
        //this is a feature (too lazy to code it out)
        if(Input.GetKey(KeyCode.Space)) {
            this.rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            this.switchStates(this.baseFactory.WallDismount());
        }
    }

    public override void checkSwitchStates(){
        if(!this.baseContext.wallCheck.onWall) {
            this.switchStates(this.baseFactory.WallDismount());
        } else if (!Input.GetKey(KeyCode.W)) {
            this.switchStates(this.baseFactory.WallDismount());
        } 
    }

}
