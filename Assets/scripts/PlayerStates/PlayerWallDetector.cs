using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWallDetector : MonoBehaviour
{
    public Transform searchPos;
    public int searchVectors = 8;
    public float searchDistance;
    public LayerMask walls;

    public PlayerController player; //big nono but fuck it we ball

    RaycastHit hit;

    public bool nearWall;
    public Vector3 wallNorm;

    void Start()
    {
        nearWall = false; 
        wallNorm = Vector3.zero;
    }

    void Update()
    {
        searchForWalls();
    }


    void searchForWalls() {
        float rot = searchPos.rotation.y;
        for(double i = rot; i < 2*Math.PI+rot; i += 2*Math.PI/(this.searchVectors)) {
            Vector3 searchDirection = new Vector3((float)(Math.Cos(i)), 0, (float)(Math.Sin(i)));
            if(Physics.Raycast(this.searchPos.position, searchDirection, out hit, searchDistance, walls) && Input.GetKey(KeyCode.Space)) {
                nearWall = true;
                player.wallRun();
                wallNorm = hit.normal;
                return;
            }
        }
        nearWall = false;
        wallNorm = Vector3.zero;
    }
}
