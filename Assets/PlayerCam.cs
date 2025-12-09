using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{

    public float sensitivity = 100f;
    public Transform playerBody;
    public Slider sensSlider;


    private float _xRot = 0f;
    private float _yRot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sensSlider.value = sensitivity;
    }

    // Update is called once per frame
    void Update()
    { 
        ChangeSens();
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _xRot -= mouseY;
        _yRot += mouseX;
        _xRot = Mathf.Clamp(_xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRot, _yRot, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }

    public void ChangeSens()
    {
        sensitivity = sensSlider.value;
    }
}
