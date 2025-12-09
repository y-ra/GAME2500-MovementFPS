using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public GameObject sound;
    public Transform player;

    public void screetch() {
        GameObject proj = Instantiate(sound, transform.position, Quaternion.identity);

        proj.transform.LookAt(player);

        Screetch scr = proj.GetComponent<Screetch>();
        
        scr.deploy();
    }
}
