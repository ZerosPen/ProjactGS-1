using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovement : Enemy
{
    [Header("Movement")]
    public float speedMovement;
    public float maxSpeedMovement;
    public int Run;
    public float distanceWaypoint;

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
    }

    public void moveToPosX(Vector2 targetPos)
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeedMovement)
        {
            if (isRoaming)
            {
                moveTowardX(targetPos);
                if (Vector2.Distance(transform.position, targetPos) < 0.1f)
                {
                    StopMove();
                    StartCoroutine(RestRoaming());
                }

            }
            else if (isChasing)
            {
                moveTowardX(targetPos);
            }
        }
    }

    public void moveTowardX(Vector2 targetPos)
    {
        Vector2 currPos = rb.position;
        Vector2 posX = new Vector2(targetPos.x, currPos.y);
        Vector2 newTarget = Vector2.MoveTowards(rb.position, posX, speedMovement * Time.deltaTime);
        rb.MovePosition(newTarget);
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
    }

    public void SetNewTargetPos()
    {
        if (WayPoints.Length < 2) return;

        int randomIndex1 = Random.Range(0, WayPoints.Length - 1);
        int randomIndex2 = 1 + randomIndex1;

        Transform pointA = WayPoints[randomIndex1];
        Transform pointB = WayPoints[randomIndex2];

        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);

        float randomX = Random.Range(minX, maxX);
        targetPosition = new Vector2(randomX, transform.position.y);
    }

    IEnumerator RestRoaming()
    {
        yield return new WaitForSeconds(1f);
        SetNewTargetPos();
    }

    IEnumerator CoolDownDodge()
    {
        canJump = false;
        yield return new WaitForSeconds(coolDownDodge);
        canJump = true;
    }
}
