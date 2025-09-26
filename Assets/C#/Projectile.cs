using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f; // àÍíËéûä‘Ç≈è¡Ç¶ÇÈ

    void Start()
    {
        OnProjectileStart();
    }

    protected virtual void OnProjectileStart()
    {
        Destroy(gameObject, lifeTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health enemy = collision.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // ìñÇΩÇ¡ÇΩÇÁè¡Ç¶ÇÈ
        }
    }
}
