using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        private Animator anim;
        private CharacterController controller;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float turnSpeed = 400.0f;
        [SerializeField] private float gravity = 20.0f;
        [SerializeField] private float jumpForce = 20f;
        [SerializeField] private float doubleJumpForce = 10f;
        private bool canDoubleJump = true;

        private Vector3 motionStep;
        private float velocity = 0f;

        /// <summary>
        /// Enables/disables the ability to move the player character via device input.
        /// </summary>
        public bool CanMove { get; set; } = true;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = gameObject.GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
            if (CanMove)
            {
                if (controller.isGrounded)
                {
                    velocity = -gravity * Time.deltaTime;
                    canDoubleJump = true; // Reset double jump ability when grounded
                }
                else
                {
                    velocity -= gravity * Time.deltaTime;
                }
            }
        }

        private void Update()
        {
            if (CanMove)
            {
                if (controller.isGrounded)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        velocity = jumpForce;
                    }
                }
                else if (canDoubleJump && Input.GetButtonDown("Jump"))
                {
                    velocity = doubleJumpForce;
                    canDoubleJump = false; // Disable double jump until grounded again
                }

                ApplyMovement();
            }

            // Animation control
            if (Input.GetKey("w") && CanMove)
            {
                anim.SetInteger("AnimationPar", 1);
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            }
        }

        private void ApplyMovement()
        {
            motionStep = Vector3.zero;
            motionStep += transform.forward * Input.GetAxisRaw("Vertical");
            motionStep += transform.right * Input.GetAxisRaw("Horizontal");
            motionStep = moveSpeed * motionStep.normalized;
            motionStep.y += velocity;
            controller.Move(motionStep * Time.deltaTime);

            // Rotation
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        }

        public void TeleportToPosition(Vector3 position)
        {
            controller.enabled = false;
            transform.position = position;
            controller.enabled = true;
        }
    }
}
