using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : Player
{
    [Header("Movement")]
    public float speedMovement;
    public float maxSpeedMovement;
    public float direction;
    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    public float dashForce;
    private float dashTime = 0.2f;
    private float coolDownDash = 1f;

    [Header("Jump")]
    private bool isJumping;
    public float jumpForce;
    public int jumpCount;
    public int maxJump;
    public Transform groundCheck;
    public float radiusGroundCheck;
    public LayerMask groundMask;
    
    [Header("KeyBind")]
    public KeyCode movementLeft = KeyCode.A;
    public KeyCode movementRight = KeyCode.D;
    public KeyCode Jump = KeyCode.Space;
    public KeyCode dash = KeyCode.LeftShift;

    private Rigidbody2D rb;
    private TrailRenderer tr;
    private bool isGroundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        direction = Input.GetKey(movementRight) ? 1f :
                    Input.GetKey(movementLeft) ? -1f : 0f;
        isGroundCheck = Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundMask);

        if (isDashing)
        {
            return;
        }

        if (isGroundCheck)
        {
            jumpCount = maxJump;
            isJumping = false;
        }
        if (Input.GetKeyDown(Jump) && isGroundCheck)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
        }
        else if (Input.GetKeyDown(Jump) && jumpCount > 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
            jumpCount--;
        }

        if (Input.GetKeyDown(dash) && canDash)
        { 
            isDashing = true;
            rb.AddForce(Vector2.right * direction * dashForce);
            StartCoroutine(CoolDownDash());
        }

        if (isDashing || isJumping)
        {
            tr.enabled = true;
        }
/*        Debug.Log("Can dash = " + canDash);*/
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (Mathf.Abs(rb.velocity.x) < maxSpeedMovement) {
            rb.AddForce(Vector2.right * direction * speedMovement);
        }
    }

    private IEnumerator CoolDownDash()
    {
        canDash = false;
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = 1f;
        tr.enabled = true;
        isDashing = false;
        yield return new WaitForSeconds(coolDownDash);
        tr.enabled = false;
        canDash = true;
    }
}
