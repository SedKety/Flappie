using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    public float mouseX, mouseY, sensitivity;
    public Vector3 lookDirectionBody, lookDirectionCamera;
    public Transform bodyTransform, cameraTransform;


    void Start()
    {
        sensitivity = 10;
    }

    void Update()
    {
        //Float wordt verbonden aan welke as hij moet pakken
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Pinnetjes systeem
        lookDirectionBody.y = mouseX;
        
        mouseY = Mathf.Clamp(mouseY, -90f, 90);
        lookDirectionCamera.x = -mouseY;
        

        //Zorgt ervoor dat ze beide kunnen rotaten 
        bodyTransform.Rotate(lookDirectionBody);
        cameraTransform.Rotate(lookDirectionCamera);

    }
}