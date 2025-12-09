using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncher : MonoBehaviour
{

    public Transform launch1;
    public Transform launch2;
    public Transform player;

    public GameObject missle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void fire() {
        GameObject left = Instantiate(missle, launch1.position, Quaternion.identity);
        GameObject right = Instantiate(missle, launch2.position, Quaternion.identity);

        left.transform.LookAt(player);
        right.transform.LookAt(player);

        Projectile leftProj = left.GetComponent<Projectile>();
        Projectile rightProj = right.GetComponent<Projectile>();
        
        leftProj.deploy();
        rightProj.deploy();
    }
}
