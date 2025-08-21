using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyMovement : Enemy
{
    [Header("Movement")]
    public float speedMovement;
    public float maxSpeedMovement;
    public int Run;

    [Header("Status")]
    public bool isRoaming;
    public bool isChasing;
    public bool canJump;
    private bool isResting;
    private int currentTargetIndex;

    /*    [Header("Dash")]
        private bool canDash = true;
        private bool isDashing;
        public float dashForce;
        private float dashTime = 0.2f;
        private float coolDownDash = 1f;*/

    [Header("Jump")]
    private bool isJumping;
    public float jumpForce;
    public Transform groundCheck;
    public float radiusGroundCheck;
    public LayerMask groundMask;
    private float coolDownDodge = 1f;

    [Header("Waypoints")]
    public Transform[] WayPoints;
    public Vector2 targetPosition;

    private Rigidbody2D rb;
    private TrailRenderer tr;
    private bool isGroundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        SetNewTargetPos();
    }

    public void moveToTarget(Vector2 targetPos)
    {
        if (isResting) return;

        float distance = Mathf.Abs(targetPosition.x - transform.position.x);

        if (distance < 0.1f)
        {
            StopMove();
            StartCoroutine(RestRoaming());
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedMovement * Time.deltaTime);
    }

    public void SetNewTargetPos()
    {
        if (WayPoints.Length < 2) return;

        float randomX;
        float minDistance = 1.5f; // how far the new target must be from current pos

        // keep trying until we find a target that's far enough
        do
        {
            randomX = Random.Range(WayPoints[0].position.x, WayPoints[1].position.x);
        }
        while (Mathf.Abs(randomX - transform.position.x) < minDistance);

        targetPosition = new Vector2(randomX, transform.position.y);
    }

    public void OnChasePlayer(Vector2 playerPos)
    {
        moveToTarget(playerPos);
    }

    public void Jump ()
    {
        if (isGroundCheck && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public void StopMove()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }

    public void OnPatrol()
    {
        moveToTarget(targetPosition);
    }

    IEnumerator RestRoaming()
    {
        if (isResting) yield break; // don't start again

        isResting = true;
        Debug.Log("IEnumerator get call");

        yield return new WaitForSeconds(3f);

        SetNewTargetPos();
        isResting = false;
    }

    IEnumerator CoolDownDodge()
    {
        canJump = false;
        yield return new WaitForSeconds(coolDownDodge);
        canJump = true;
    }
}
