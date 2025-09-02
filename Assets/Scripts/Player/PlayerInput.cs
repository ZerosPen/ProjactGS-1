using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public CharacterDataSO CharacterData;
    private MovementCharacter _movementCharacter;
    private bool isFacingRight;
    private bool isFalling;
    

    [Header("Ground Checker")]
    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    private Vector2 directionPlayer;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _movementCharacter = GetComponent<MovementCharacter>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        CharacterData.CountDash = CharacterData.MaxDashCount;
    }

    private void FixedUpdate()
    {
        if (directionPlayer.x != 0)
        {
            _movementCharacter.OnWalking(directionPlayer.x);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, _movementCharacter.radiusGroundCheck, groundMask);

        if (isGrounded)
        {
            CharacterData.canJump = true;
        }

        // Dash recharge
        if (CharacterData.CountDash < CharacterData.MaxDashCount && !CharacterData.isRecharging)
        {
            StartCoroutine(RechargeDash());
        }
        Debug.Log($"Dash Count = {CharacterData.CountDash}");

        // Walking animation
        _animator.SetFloat("speedMove", _movementCharacter.SpeedMovement());

        if (directionPlayer.x != 0)
        {
            _spriteRenderer.flipX = isFacingRight;
        }

        isFalling = _movementCharacter.SpeedFalling() < 0;
        //Falling animation
        if (isFalling)
        {
            _animator.SetBool("isFalling", true);
        }
        else
        {
            _animator.SetBool("isFalling", false);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        directionPlayer = context.ReadValue<Vector2>();

        if (_movementCharacter == null)
            Debug.LogWarning($"Script MovementCharacter is missing or not assign yet in {gameObject.name}!");

        if (directionPlayer.x > 0)
        {
            isFacingRight = false;
        }
        else
        {
            isFacingRight = true;
        }
    }

    public void OnJumpping(InputAction.CallbackContext context)
    {
        if (_movementCharacter != null && context.performed)
        {
            if (CharacterData.canDash)
            {
                _movementCharacter.OnJump(isGrounded);
                _animator.SetTrigger("isJumping");
            }
        }
    }

    public void OnDashing(InputAction.CallbackContext context)
    {
        if (_movementCharacter != null && context.performed)
        {
            _movementCharacter.OnDash(directionPlayer);
        }
    }

    private IEnumerator RechargeDash()
    {
        CharacterData.isRecharging = true; ;
        yield return new WaitForSeconds(3f);
        CharacterData.CountDash++;
        // Keep recharging until full
    if (CharacterData.CountDash < CharacterData.MaxDashCount)
        {
            StartCoroutine(RechargeDash());
        }
        else
        {
            CharacterData.isRecharging = false;
        }
    }

    public Vector2 GetDirection()
    {
        return directionPlayer;
    }
}
