using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region
    public static Transform instance;

    private void Awake()
    {
        instance = this.transform;
    }
    #endregion
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpForce;

    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    [SerializeField] private float gravity;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isCharacterGrounded = false;
    private Vector3 velocity = Vector3.zero;

    private PlayerStats stats;

    private void Start() 
    {
        GetReference();
        InitVariables();
    }

    private void Update() 
    {
        HandleIsGrounded();
        HandleJump();
        HandleGravity();
        HandleRunning();
        HandleMovement();

    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = moveDirection.normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        if(!stats.IsDead())
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void GetReference()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }

    private void InitVariables()
    {
        moveSpeed = walkSpeed;
    }

    private void HandleRunning()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
    }

    private void HandleGravity() 
    {
        if(isCharacterGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isCharacterGrounded) 
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    private void HandleIsGrounded()
    {
        isCharacterGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
    }
}
