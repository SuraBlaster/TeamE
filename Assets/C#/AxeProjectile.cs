using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class AxeProjectile : Projectile
{
    public float throwForce = 10f;          // �������
    public float shockwaveRadius = 3f;      // �Ռ��g�͈̔�
    public GameObject shockwavePrefab;      // �Ռ��g�̃G�t�F�N�gPrefab

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // �����_���ȕ����ɕ��𓊂���
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDir * throwForce, ForceMode2D.Impulse);

        // ��莞�Ԍ�ɏ����鏈���͊��Projectile��Start()�œ���
        base.OnProjectileStart();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �Ռ��g���o��
        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        }

        // �͈͓��̓G�Ƀ_���[�W
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy")) // Tag�Ŕ���
            {
                Health enemy = hit.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject); // ���{�̂͏�����
    }

    // �V���b�N�E�F�[�u�͈̔͂�Scene�r���[�Ō�����悤��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
    }
}