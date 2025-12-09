using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public float destroyTimer;
    public LayerMask ground;
    public void Start() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 10f, ground)) {
            transform.position = hit.point;
        }
        Invoke(nameof(destroy), destroyTimer);
    }

    private void destroy() {
        Destroy(gameObject);
    }
}
