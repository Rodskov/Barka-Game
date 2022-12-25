using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    public float walkSpeed = 3;
    public float runSpeed = 5;
    private float horizontalInput;
    private float verticalInput;

    private float actionDelay = 2f;
    private float nextActionTime = 0f;

    // Controls movement
    private Vector3 moveUpDown;
    private Vector3 moveLeftRight;
    private Vector3 velocity;

    // For the player to not be stuck in the air
    private bool isGrounded;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;
    private float gravity = -9.81f;

    // To trigger certain things
    private CharacterController controller;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();

        // Pertains to player actions; Coroutine is used with IEnumerator
        if(Time.time >= nextActionTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(AttackLeft());
                nextActionTime = Time.time + 1f / actionDelay;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartCoroutine(AttackRight());
                nextActionTime = Time.time + 1f / actionDelay;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Defend());
        }
    }

    void Move()
    {
        // Checks if player is on the ground
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Player Movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Moves based on the player's axis, not the world's axis
        moveUpDown = new Vector3(0, 0, verticalInput);
        moveUpDown = transform.TransformDirection(moveUpDown);
        moveLeftRight = new Vector3(horizontalInput, 0, 0);
        moveLeftRight = transform.TransformDirection(moveLeftRight);

        // Player can only move if on the ground, divided into other functions for easy use of animations
        if (isGrounded)
        {
            if (moveUpDown != Vector3.zero || moveLeftRight != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }

            else if (moveUpDown != Vector3.zero || moveLeftRight != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }

            else if (moveUpDown == Vector3.zero || moveLeftRight == Vector3.zero)
            {
                Idle();
            }
        }

        controller.Move(moveUpDown * moveSpeed * Time.deltaTime);
        controller.Move(moveLeftRight * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Walk", 0.5f, 0.1f, Time.deltaTime);
    }

    void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Walk", 1, 0.1f, Time.deltaTime);
    }

    void Idle()
    {
        anim.SetFloat("Walk", 0, 0.1f, Time.deltaTime);
    }
    // Allows smooth animation for player controls while moving; Uses Avatar Mask
    private IEnumerator AttackLeft()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("AttackLeft");

        yield return new WaitForSeconds(2f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }

    private IEnumerator AttackRight()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("AttackRight");

        yield return new WaitForSeconds(2f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
    private IEnumerator Defend()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Defend");

        yield return new WaitForSeconds(2f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
}
