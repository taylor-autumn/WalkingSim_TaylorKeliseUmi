using System;
using TMPro;
using UnityEngine;

public class movementScript : MonoBehaviour
{

    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;
    public float gravity = 20.0f;

    private bool canMove = true;

    [Header("Mouse Look")]
    private Camera cam;
    public float mouseSensitivity = 2.0f;
    public float verticalLookLimit = 80.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    public bool debugs;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
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

    }

}
