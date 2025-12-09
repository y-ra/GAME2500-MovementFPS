using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappleState : PlayerBaseState
{
    private float grappleStrafe;
    public float yankForce;
    public GrapplePoint point;
    public float ropeLength;

    public float ropeSpringK;
    public float ropeDamper;

    public PlayerGrappleState(PlayerController context, PlayerStateFactory factory, Rigidbody rigid, float speed, GrapplePoint point, float grappleStrafe, float ropeLength, float yankForce, float springK, float damper)  : base (context, factory, speed, rigid){
        this.point = point;
        this.yankForce = yankForce;
        this.ropeLength = ropeLength;
        this.grappleStrafe = grappleStrafe;
        this.ropeSpringK = springK;
        this.ropeDamper = damper;
    }

    public override void enterState(){
        this.rigid.drag = 0;


        this.rigid.AddForce((point.transform.position - this.baseContext.transform.position).normalized * yankForce, ForceMode.Impulse);
    }

    public override void updateState(){
        checkSwitchStates();
    }

    public override void exitState(){
        this.rigid.drag = this.baseContext.groundDrag;
    }

    public override void checkSwitchStates(){
        if(!Input.GetKey(KeyCode.Q)) {
            this.switchStates(this.baseFactory.Air());
        } 
    }

    public override bool requestSwitchStates(PlayerBaseState swapTo) {
        return false;
    }


    //can't jump while swinging
    public override void jump() {}

    public override void movePlayer(Vector3 desiredDirection) {
        
        Vector3 distance = point.transform.position - this.baseContext.transform.position;
        Debug.Log("ROPE DISTANCE: " + distance.magnitude);
        float ropeStretch = distance.magnitude - this.ropeLength;

        if(distance.magnitude > this.ropeLength) {

            float theta = Vector3.Angle(distance, Vector3.up);
            // Vector3 planeForce = new Vector3(Mathf.Abs(distance.x)*distance.x, Mathf.Cos(theta)*Mathf.Cos(theta)*Mathf.Abs(distance.y)*distance.y, Mathf.Abs(distance.z)*distance.z);
            Vector3 planeForce = new Vector3(distance.x, Mathf.Cos(theta)*distance.y, distance.z);
            // planeForce = Vector3.ProjectOnPlane(this.rigid.velocity, planeForce);
            planeForce = Vector3.ProjectOnPlane((point.transform.position - this.baseContext.transform.position), planeForce);

            Vector3 springForce = new Vector3(distance.x, distance.y, distance.z).normalized*ropeStretch*this.ropeSpringK - this.rigid.velocity * this.ropeDamper;

            this.rigid.AddForce(planeForce * ropeStretch + springForce, ForceMode.Force);
            // this.rigid.AddForce(planeForce * ropeStretch, ForceMode.Force);
        }

        rigid.AddForce(desiredDirection.normalized * this.maxSpeed * 10f * grappleStrafe, ForceMode.Force);
    }

    public override Vector3 capSpeed() {
        return (this.rigid.velocity.magnitude > this.maxSpeed) ? this.rigid.velocity.normalized * this.maxSpeed : this.rigid.velocity;
    }
}
