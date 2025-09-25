using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 3f; // ˆê’èŠÔ‚ÅÁ‚¦‚é

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = GetComponent<Health>();

            health.TakeDamage(damage);

            Destroy(gameObject); // “–‚½‚Á‚½‚çÁ‚¦‚é
        }
    }
}
