using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShot : Projectile
{ 
    public float speed = 10f;       // ”ò‚Ô‘¬‚³

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health enemy = collision.gameObject.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // “–‚½‚Á‚½‚çÁ‚¦‚é
        }
    }
}
