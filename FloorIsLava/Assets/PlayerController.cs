using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float gravity = 30f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float doubleJumpForce = 10f;
    [SerializeField] private float defaultMoveSpeed = 3.5f;

    private Vector3 motionStep;
    private bool jumpedTwice = false;
    private float velocity = 0f;
    private float currentSpeed = 0f;
    private CharacterController controller;

    /// <summary>
    /// Enables/disables the ability to move the player character via device input.
    /// </summary>
    public bool CanMove { get; set; } = true;

    /// <summary>
    /// Awake is called before the Start.
    /// </summary>

    private void Awake()
    {
        TryGetComponent(out controller);
    }
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        currentSpeed = defaultMoveSpeed;
    }

    /// <summary>
    /// FixedUpdate may be called more than one per frame. 
    /// </summary>
    private void FixedUpdate()
    {
        if(CanMove == true)
        {
            if(controller.isGrounded == true)
            {
                velocity = -gravity * Time.deltaTime;
            }
            else
            {
                velocity -=gravity * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(CanMove == true)
        {
            if(controller.isGrounded == true)
            {
                if(jumpedTwice == true)
                {
                    jumpedTwice = false;
                }
                if (Input.GetButtonDown("Jump") == true)
                {
                    velocity = jumpForce;
                }
            }
            else if(jumpedTwice == false)
            {
                if (Input.GetButtonDown("Jump") == true)
                {
                    jumpedTwice = true;
                    velocity = doubleJumpForce;
                }
            }
            ApplyMovement();
        }
    }
    private void ApplyMovement()
    {
        motionStep = Vector3.zero;
        motionStep += transform.forward * Input.GetAxisRaw("Vertical");
        motionStep += transform.right * Input.GetAxisRaw("Horizontal");
        motionStep = currentSpeed * motionStep.normalized;
        motionStep.y += velocity;
        controller.Move(motionStep * Time.deltaTime);
    }
    public void TeleportToPosition(Vector3 position)
    {
        controller.enabled = false;
        transform.position= position;
        controller.enabled = true;
    }
}
