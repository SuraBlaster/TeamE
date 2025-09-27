using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleShot : Projectile
{
    public float speed = 10f;       // ”ò‚Ô‘¬‚³
    public GameObject BlackHolePrefab;      
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        base.OnProjectileStart();
    }

    public void Launch(Vector2 direction)
    {
        if (rb != null)
        {
            rb.velocity = direction.normalized * speed;
        }

        // Œ©‚½–Ú‚ÌŒü‚«‚ğis•ûŒü‚É‡‚í‚¹‚é
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Tag‚Å”»’è
        {
            // ÕŒ‚”g‚ğo‚·
            if (BlackHolePrefab != null)
            {
                Instantiate(BlackHolePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
