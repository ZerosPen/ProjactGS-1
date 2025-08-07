using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMove : Player
{
    public Vector2 movementInput;
    private Rigidbody2D rb;
    public float speed;
    private TrailRenderer tr;


    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    public float powerDash;
    private float dashTime = 0.2f;
    private float coolDownDash = 1f;

    [Header("Jump")]
    [SerializeField] private bool isJumping;
    [SerializeField] private bool canJump;   
    public float jumpForce;
    public Transform groundCheck;
    public float radiusGroundCheck;
    public LayerMask groundMask;
    [SerializeField] private bool isGroundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        rb.velocity = new Vector2 (movementInput.x * speed, rb.velocity.y);
        isGroundCheck = Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundMask);

        if (isGroundCheck)
        {
            canJump = true;
        }

    }

    public void onMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDashing = true;
            rb.AddForce(Vector2.right * movementInput * powerDash);
            StartCoroutine(CoolDownDash());
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canJump)
            {
                canJump = false;
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    private IEnumerator CoolDownDash()
    {
        canDash = false;
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = 1f;
        isDashing = false;
        yield return new WaitForSeconds(coolDownDash);
        canDash = true;
    }
}
