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
    [SerializeField] private float dirX;


    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    public float powerDash;
    private float dashTime = 0.2f;
    private float coolDownDash = 1f;

    [Header("Jump")]
    [SerializeField] private bool isJumping;
    [SerializeField] private bool canJump;
    private bool isFalling;
    public float jumpForce;
    public Transform groundCheck;
    public float radiusGroundCheck;
    public LayerMask groundMask;
    [SerializeField] private bool isGroundCheck;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(movementInput.x * speed, rb.velocity.y);
        isGroundCheck = Physics2D.OverlapCircle(groundCheck.position, radiusGroundCheck, groundMask);

        isFalling = rb.velocity.y < 0;

        if (isGroundCheck)
        {
            canJump = true;
            _animator.SetBool("isFalling", false);
        }

        if (isFalling)
        {
            _animator.SetBool("isFalling", isFalling);
        }

    }

    public void onMovement(InputAction.CallbackContext context)
    {
        if (!UIManager.Instance.isPanelOpen)
        {
            movementInput = context.ReadValue<Vector2>();
            dirX = movementInput.x;

            if (dirX < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }

            if (dirX > 0 || dirX < 0)
            {
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }

        Debug.Log(movementInput);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && !UIManager.Instance.isPanelOpen)
        {
            isDashing = true;
            rb.AddForce(Vector2.right * movementInput * powerDash);
            StartCoroutine(CoolDownDash());
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed && !UIManager.Instance.isPanelOpen)
        {
            if (canJump)
            {
                canJump = false;
                _animator.SetTrigger("isJumping");
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
