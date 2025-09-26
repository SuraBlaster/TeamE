using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class AxeProjectile : Projectile
{
    public GameObject shockwavePrefab;   // �Ռ��g�̃v���n�u
    public float shockwaveRadius = 2f;   // �Ռ��g�̔��a
    public LayerMask enemyLayer;         // �G�̃��C���[����p

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����n�ʂ�G�ɒ��e�����Ƃ�
        Explode();
    }

    private void Explode()
    {
        // �Ռ��g�̐����i�A�j���[�V�����t��Prefab�j
        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        }

        // �͈͓��̓G������
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius, enemyLayer);
        foreach (var hit in hits)
        {
            Health enemy = hit.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Projectile����p������damage
            }
        }

        Destroy(gameObject); // ���{�̂͏�����
    }
}