using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool doorMoving = false;
    public float rotationSpeed;
    public bool isOpened;



    public void OnTriggerStay(Collider c) {
        if (c.transform.parent != null && c.transform.parent.gameObject.name == "Player" && Input.GetKey(KeyCode.E) && !doorMoving) {
            doorMoving = true;
            StartCoroutine(moveDoor());
        }
    }




    public IEnumerator moveDoor() {
        float targetRotation = isOpened ? (360+transform.eulerAngles.y + 90f)%360 : (360+transform.eulerAngles.y - 90f)%360;
        while (transform.eulerAngles.y < targetRotation && isOpened || transform.eulerAngles.y > targetRotation && !isOpened)
        {
            Debug.Log("ANGLE: " + transform.eulerAngles.y + ", " + targetRotation);
            float rotationAmount = (isOpened) ? rotationSpeed * Time.deltaTime : -rotationSpeed * Time.deltaTime;
            Debug.Log("AMOUNT: " + rotationAmount + ", " + isOpened);
            transform.Rotate(Vector3.up * rotationAmount);
            yield return null;
        }

        doorMoving = false;
        isOpened = !isOpened;
    }
}
