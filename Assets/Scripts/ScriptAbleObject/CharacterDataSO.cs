using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("CharacterDataSO"), fileName = "CharacterDataSO")]
public class CharacterDataSO : ScriptableObject
{
    [Header("walk/Run Movement Settings")]
    public float WalkMovement;
    public float smootherLerp;
    public float accelrationMovement;
    public float MaxSpeedMovement;

    [Header("Jump Settings")]
    public bool canJump;
    public float JumpHeight;
    public float CountJump;
    public int MaxJumpCount;

    [Header("Gravity Settings")]
    public float GravityScale;
    public float FallingGravityMultiplier;
    public float MaxVelocity;

    [Header("Dash Settings")]
    public bool canDash;
    public bool isDashing;
    public bool isRecharging;
    public float DashPower;
    public int MaxDashCount;
    public int CountDash;
    public float CoolDownDash;
    public float TimeDash;

}
