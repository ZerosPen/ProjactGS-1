using System.Collections;
using UnityEngine;

public class EnemyMovement : Enemy
{
    private bool isResting;
    public CharacterDataSO characterData { get; private set; }

    [Header("Waypoints")]
    public Transform[] WayPoints;
    public Vector2 targetPosition;
    public float distance;

    private Rigidbody2D rb;
    private TrailRenderer tr;
    private MovementCharacter _movementCharacter;
    private bool isGroundCheck;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        _movementCharacter = GetComponent<MovementCharacter>();
        SetNewTargetPos();
    }

    public void moveToTarget(Vector2 targetPos)
    {
        if (isResting) return;

        distance = Mathf.Abs(targetPos.x - transform.position.x);

        if (distance < 0.1f)
        {
            StopMoving();
            StartCoroutine(RestRoaming());
            return;
        }
        // get direction (-1 for left, +1 for right)
        float direction = Mathf.Sign(targetPos.x - transform.position.x);

        _movementCharacter.OnWalking(direction);
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

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void OnPatrol()
    {
        moveToTarget(targetPosition);
    }

    public IEnumerator RestRoaming()
    {
        if (isResting) yield break;

        isResting = true;
        Debug.Log("Enemy is resting...");

        yield return new WaitForSeconds(3f);

        isResting = false;
        SetNewTargetPos();
    }

    public bool IsResting => isResting;
}
