using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMovement : MonoBehaviour
{
    public float moveSpeed, sprintSpeed, startSpeed, horizontalMovement, verticalMovement, stamina;
    public bool sprint;
    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        startSpeed = 10;
        moveSpeed = startSpeed;
        sprintSpeed = 30;
        sprint = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Float wordt verbonden aan welke as hij moet pakken
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // movement.x is gelijk aan horizontalmovement dus dat betekent dat hij aangeeft dat hij op de horizontal as kan bewegen.
        movement.x = horizontalMovement;
        movement.z = verticalMovement;

        //De transform.Translate zorgt ervoor dat je de goede kant op kan lopen
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Als je de key linker shift indrukt dan veranderd de speed 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("sprint");
            moveSpeed = sprintSpeed;
        }

        else
        {
            //.Log("notsprint");
            moveSpeed = startSpeed;
        }


    }
}