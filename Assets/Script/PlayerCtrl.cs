using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCtrl : MonoBehaviour
{

    /*float mousex = 0f;
    float mousey = 0f;
    float h = 0;
    float v = 0;

    private float rotSpeed = 3f;

    private float MinX = -50f;
    private float MaxX = 50;
    private float rotX = 0f;
    private float rotY = 0f;

    public float moveSpeed = 3f;

    private CharacterController characterController;
    public float gravity = 150.0f;
    Vector3 moveDirection = Vector3.zero;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotate();
        UpdateMove();
    }

    void UpdateRotate()
    {
        mousex = Input.GetAxis("MouseX");
        mousey = Input.GetAxis("MouseY");

        Updaterotate(mousex, mousey);
    }

    void UpdateMove()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        h = moveSpeed*Input.GetAxis("Horizontal");
        v = moveSpeed*Input.GetAxis("Vertical");

        moveDirection = (forward * v) + (right * h);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

    }

    public void Updaterotate(float mouseX, float mouseY)
    {
        rotY += mouseX * rotSpeed;
        rotX -= mouseY * rotSpeed;

        rotX = Mathf.Clamp(rotX, MinX, MaxX);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }*/

    public Camera playerCamera;
    public float walkSpeed = 1.15f;
    public float runSpeed = 4.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float gravity = 150.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("MouseY") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("MouseX") * lookSpeed, 0);
        }
    }
}
