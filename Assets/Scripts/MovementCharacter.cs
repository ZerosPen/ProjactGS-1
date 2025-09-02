using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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

    public void OnWalking(float dirX)
    {
        // Target horizontal speedMovement
        float targetSpeed = dirX * CharacterData.WalkMovement;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ?
            CharacterData.accelrationMovement :
            CharacterData.accelrationMovement * 2f;

        // Instead of always lerping from 0, lerp between current velocity and target speedMovement
        float desiredSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, CharacterData.smootherLerp * Time.fixedDeltaTime);

        // Calculate difference again after smoothing
        float speedDifference = desiredSpeed - rb.velocity.x;

        rb.AddForce(Vector2.right * speedDifference * accelRate, ForceMode2D.Force);

        // Clamp velocity to max speedMovement
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
