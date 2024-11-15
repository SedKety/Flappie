using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    private BunnyInput bunnyInput;
    private InputAction move, rotate;

    public Rigidbody rb;
    public Camera camHolder;

    public int amountOfHead;
    public float jumpForce, sensitivity, moveSpeed, sprintspeed, currentSpeed;

    public Vector3 forceDirection;
    public Vector3 jump;

    private Vector2 look;
    private float x, y;

    public Animator animator;

    public bool isGrounded, jumping, spawnedYet;


    RaycastHit hit;
    public LayerMask groundLayer;

    public AudioSource walkAudio;

    private void Awake()
    {
        Instance = this;
        bunnyInput = new BunnyInput();

        jump = new Vector3 (0, 4, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = moveSpeed;
        sensitivity = PlayerPrefs.GetFloat("sensitivity");

        if (sensitivity < 0.02f) 
        {
            sensitivity = 0.4f;
        }
    }

    private void OnEnable()
    {
        move = bunnyInput.Bunny.Move;
        move.Enable();
        rotate = bunnyInput.Bunny.Mouse;
        bunnyInput.Bunny.Mouse.Enable();
        rotate.Enable();
        animator.SetTrigger("idle");
    }

    private void OnDisable()
    {
        move.Disable();
        rotate.Disable();
    }


    private void Update()
    {
        Rotate();
    }
    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(camHolder) * currentSpeed;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(camHolder) * currentSpeed;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * 3.5f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            {
                if (hit.transform.GetComponent<Item>())
                {
                    hit.transform.GetComponent<Item>().PickUpItem();
                }
                else if (hit.transform.GetComponent<OfferAltar>())
                {
                    OfferAltar altar = hit.transform.GetComponent<OfferAltar>();
                    if (amountOfHead > 0 && !altar.hasAdded)
                    {
                        altar.AddSacrifice();
                        amountOfHead--;
                    }
                }
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.9f))
        {
            if (hit.transform.tag == "Jumpable")
            {
                //return;
                if (!isGrounded)
                {
                    isGrounded = true;
                    jumping = false;
                }
            }
        }
        else
        {
            isGrounded = false;
        }

        //animations
       
        animator.SetFloat("Speed", currentSpeed);
        if(currentSpeed == 0)
        {
            animator.SetTrigger("Idle");
            //animator.SetFloat("Speed", currentSpeed);
        }

        //audio

        if (isGrounded)
        {
           walkAudio.Play();
        }
    }

    private Vector3 GetCameraForward(Camera cam)
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }
    private Vector3 GetCameraRight(Camera cam)
    {
        Vector3 right = cam.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    private void Rotate()
    {
        look = rotate.ReadValue<Vector2>();

        x += look.x * sensitivity;
        y -= look.y * sensitivity;

        y = Mathf.Clamp(y, -85, 85);

        transform.localRotation = Quaternion.Euler(0, x, 0 * Time.deltaTime);
        camHolder.transform.localRotation = Quaternion.Euler(y, 0, 0 * Time.deltaTime);
    }
    public void DoJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (isGrounded)
            {
                jumping = true;
                isGrounded = false;
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                animator.SetTrigger("jump");
            }

        }
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSpeed = moveSpeed + sprintspeed;
            animator.SetFloat("Speed", currentSpeed);

        }
        else
        {
            currentSpeed = moveSpeed;
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    public IEnumerator LocateGround()
    {
        while (spawnedYet == false)
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 100, 0), Vector3.down))
            {
                transform.position = hit.point;
                spawnedYet = true;
            }
            yield return new WaitForEndOfFrame();
        }      
    }
}
