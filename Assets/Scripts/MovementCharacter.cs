using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Controls the movement behavior of a character including walking, jumping, and dashing.
/// Uses Rigidbody2D physics for movement and relies on CharacterDataSO for configuration.
/// </summary>
public class MovementCharacter : MonoBehaviour
{
    public CharacterDataSO CharacterData;
    public float radiusGroundCheck;
    [SerializeField] private float speedMovement;
    [SerializeField] private float speedFalling;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check horizontal speedMovement from Rigidbody2D
        speedMovement = Mathf.Abs(rb.velocity.x);

        // Check Vertical speedFalling from Rigidbody2D
        speedFalling = rb.velocity.y;
    }

    /* <summary>
        Handles horizontal walking movement.
        Smoothly accelerates or decelerates the character towards the target speed based on input direction.
        </summary>
        <param name="dirX">Horizontal input direction (-1 for left, 1 for right, 0 for no input).</param>*/
    public void OnWalking(float dirX)
    {
        // Calculate target horizontal speed based on input and character walk speed
        float targetSpeed = dirX * CharacterData.WalkMovement;

        // Determine acceleration rate: faster acceleration when stoppin
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ?
            CharacterData.accelrationMovement :
            CharacterData.accelrationMovement * 2f;

        // Smoothly interpolate current velocity towards target speed
        float desiredSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, CharacterData.smootherLerp * Time.fixedDeltaTime);

        // Calculate speed difference after smoothing
        float speedDifference = desiredSpeed - rb.velocity.x;

        rb.AddForce(Vector2.right * speedDifference * accelRate, ForceMode2D.Force);

        // Clamp horizontal velocity to max speed
        if (Mathf.Abs(rb.velocity.x) > CharacterData.MaxSpeedMovement)
        {
            rb.velocity = new Vector2(
                Mathf.Sign(rb.velocity.x) * CharacterData.MaxSpeedMovement,
                rb.velocity.y
            );
        }
    }

    public void OnJump(bool isGrounded)
    {
        if (isGrounded)
        {
            CharacterData.CountJump = CharacterData.MaxJumpCount;
        }

        if (CharacterData.CountJump > 0)
        {
            float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
            float jumpVelocity = Mathf.Sqrt(2 * gravity * CharacterData.JumpHeight);

            // Set upward velocity directly
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            CharacterData.CountJump--;
            CharacterData.canJump = false;
        }
    }

    public void OnDash(Vector2 direction)
    {
        Debug.Log($"the script is get call by other script!");

        if (CharacterData.CountDash > 0)
        {
            rb.AddForce(Vector2.right * direction.x * CharacterData.DashPower, ForceMode2D.Impulse);
            CharacterData.CountDash--;
            CharacterData.canDash = false;
            CharacterData.isDashing = true;
            StartCoroutine(CoolDownDash());
        }
    }

    private IEnumerator CoolDownDash()
    {
        CharacterData.canDash = false;
        yield return new WaitForSeconds(CharacterData.TimeDash);
        CharacterData.isDashing = false;
        yield return new WaitForSeconds(CharacterData.CoolDownDash);
        CharacterData.canDash = true;
    }

    public float SpeedMovement()
    {
        return speedMovement;
    }

    public float SpeedFalling()
    {
        return speedFalling;
    }
}
