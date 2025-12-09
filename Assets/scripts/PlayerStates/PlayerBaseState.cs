using System;
using UnityEngine;
//probably awful but i want to have exceptions so unlucky


//represents a state a player can have in the game
public abstract class PlayerBaseState
{
    protected PlayerController baseContext;
    protected PlayerStateFactory baseFactory;
    protected float maxSpeed;
    protected Rigidbody rigid;

    public PlayerBaseState(PlayerController context, PlayerStateFactory factory, float topSpeed, Rigidbody playerRB) {
        baseContext = context;
        baseFactory = factory;
        maxSpeed = topSpeed;
        rigid = playerRB;
    }



    public abstract void enterState();
    
    public abstract void updateState();

    public abstract void exitState();

    public abstract void checkSwitchStates();

    public abstract bool requestSwitchStates(PlayerBaseState swapTo);



    public virtual void jump() {
        this.rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
        this.rigid.AddForce(Vector3.up * this.baseContext.jumpForce, ForceMode.Impulse);
    }

    public virtual void movePlayer(Vector3 desiredDirection) {
        rigid.AddForce(desiredDirection.normalized * maxSpeed * 10f, ForceMode.Force);
        
    }

    public virtual Vector3 capSpeed() {
        Vector3 planeSpeed = Vector3.ProjectOnPlane(this.rigid.velocity, this.baseContext.slopeHitNormal);
        Vector3 normalSpeed = this.rigid.velocity - planeSpeed;

        if(planeSpeed.magnitude > this.maxSpeed) {
            planeSpeed = planeSpeed.normalized * this.maxSpeed;
        }

        return planeSpeed + normalSpeed;
    }

    
    protected void switchStates(PlayerBaseState newState){
        exitState();
        newState.enterState();
        
        baseContext.state = newState;
    }
}
