using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Status skills")]
    public float damage;
    public float speed;
    public float direction;
    public GameObject explosionVFX;
    private GameObject explosionActive;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(direction * speed, 0f) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            GameObject ExplosionVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            float duration = ExplosionVFX.GetComponent<ParticleSystem>().main.duration;

            Destroy(ExplosionVFX, duration);
            gameObject.SetActive(false);
            return;
        }

        Idamagetable damageTable = collision.GetComponent<Idamagetable>();
        if (damageTable == null || collision.CompareTag("player")) return;
        {
            Debug.Log(collision.name);
            GameObject ExplosionVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            float duration = ExplosionVFX.GetComponent<ParticleSystem>().main.duration;

            damageTable.TakeDamage(damage);
            Destroy(ExplosionVFX, duration);
            gameObject.SetActive(false);
            Debug.Log("Enemy hit " + damage);
        }
    }
}
