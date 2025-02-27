using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook: MonoBehaviour{
    public enum RotationAxis{
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxis axes = RotationAxis.MouseXandY;
    public float horSensitivity = 9f;
    public float vertSensitivity = 9f;
    public float minimumVert = -45f;
    public float maximumVert = 45f;
    public float verticalRot = 0f;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body!=null) {
            body.freezeRotation = true;
        }
    }
    void Update(){
        if(axes == RotationAxis.MouseX){
            //Horizontal rotation
            transform.Rotate(0, horSensitivity * Input.GetAxis("Mouse X"), 0);
        }else if (axes == RotationAxis.MouseY){
            // Vertical rotation
            //transform.Rotate(vertSensitivity * Input.GetAxis("Mouse Y"),0,0);
            verticalRot -= vertSensitivity * Input.GetAxis("Mouse Y");
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0) ;
        
        } else{
            // Horizontal and vertical rotation
            verticalRot -= Input.GetAxis("Mouse Y") * vertSensitivity;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * horSensitivity;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3 (verticalRot, horizontalRot, 0);
        }

    }
}
