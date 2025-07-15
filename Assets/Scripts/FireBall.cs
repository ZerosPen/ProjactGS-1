using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Status skills")]
    public float damage;
    public float speed;
    public float direction;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(direction * speed, 0f) * Time.deltaTime;
        Debug.Log("speed" + rb.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
