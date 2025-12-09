using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ok listen this is already a state but I don't want to rewrite state machine to allow layered states so we will have this instead :))))
public class Tether : MonoBehaviour
{
    public bool latched;
    public Player player;
    public Transform tetherPoint;
    public float correctingForce;
    public LineRenderer teth;
    public float tetherRange;

    public void latch() {
        latched = true;
        teth.positionCount = 2;
    }

    public void Update() {
        if(latched) {
            //draw line
            teth.SetPosition(0,transform.position);
            teth.SetPosition(1,player.transform.position);

            if((player.transform.position - tetherPoint.position).magnitude > tetherRange) {
                float mag = correctingForce * ((player.transform.position - tetherPoint.position).magnitude - tetherRange);
                player.controller.launch((tetherPoint.position - player.transform.position).normalized, mag);
            }
        }
    }

    public void unlatch() {
        teth.positionCount = 0;
        latched = false;
    }
}
