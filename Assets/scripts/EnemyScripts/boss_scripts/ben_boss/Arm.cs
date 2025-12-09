using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//yes I have already made thsi script 3 times
//no I will not abstract this
public class Arm : MonoBehaviour
{
    public GameObject disk;
    public Transform player;

    public void deploy() {
        GameObject proj = Instantiate(disk, transform.position, Quaternion.identity);

        proj.transform.LookAt(player);

        ThrowableDisk scr = proj.GetComponent<ThrowableDisk>();
        
        scr.deploy();
    }
}
