using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{

    public bool onWall;

    void OnTriggerStay(Collider c) {

        if (c.gameObject.layer == LayerMask.NameToLayer("RunnableWall"))
        {
            onWall = true;
        }
    }

    void OnTriggerExit(Collider c) {
        onWall = false;
    }
}
