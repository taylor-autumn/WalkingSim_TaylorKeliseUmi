using System;
using TMPro;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public gameManager managerRef;
    public GameObject respawnPoint;

    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    [Header("Bools and Elevator shit")]
    private bool canMove = true;
    public bool inElev = false;
    public bool notMoving = true;
    public elevator elevRef;

    [Header("Mouse Look")]
    private Camera cam;
    public float mouseSensitivity = 2.0f;
    public float verticalLookLimit = 80.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    unitInfo charUnit;
    interactCircle colliderRef;

    public bool debugs;

    //me shit
    public static bool canPeesh = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        //this also means you cant move the camera, decide later if we want it that way or not

        if (managerRef.state == gameState.normalMode)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }


        if (canMove)
        {

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);



            float curSpeedX = moveSpeed * Input.GetAxis("Vertical");
            float curSpeedY = moveSpeed * Input.GetAxis("Horizontal");

            moveDirection.y -= gravity * Time.deltaTime;


            characterController.Move(moveDirection * Time.deltaTime);

            if (characterController.isGrounded)
            {
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }

            if (cam != null)
            {

                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

                transform.Rotate(0, mouseX, 0);

                rotationX -= mouseY;
                rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);
                cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            }

        }

        if (!inElev && notMoving && Input.GetKeyDown(KeyCode.J))
        {
            print("opening doors, this would be like pushing the button");

            GameObject elevator = GameObject.Find("elevatorGO");
            Animator elevAnim = elevator.GetComponent<Animator>();
            elevAnim.SetTrigger("open");

        }

        if (inElev && notMoving && Input.GetKeyDown(KeyCode.K))
        {
            print("closing and moving elevator");
            GameObject elevator = GameObject.Find("elevatorGO");
            Animator elevAnim = elevator.GetComponent<Animator>();

            elevAnim.SetTrigger("close");
            Invoke(nameof(moveFloors), 3f);

            //setting the moving false to signify we are moving now
            notMoving = false;

        }

    }

    public void moveFloors()
    {
        GameObject elevFloor = GameObject.Find("elevatorFloor");
        Animator moveElevAnim = elevFloor.GetComponent<Animator>();
        moveElevAnim.SetTrigger("move");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("elevator"))
        {
            inElev = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("elevator"))
        {
            inElev = false;
            notMoving = true;
        }
    }


}
