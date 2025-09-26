using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 0.5f;
    public float maxRadius = 1f;

    private CircleCollider2D col;
    private float timer = 0f;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;
        col.radius = Mathf.Lerp(0f, maxRadius, t); // ���Ԃɍ��킹�čL����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health enemy = collision.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (col != null)
        {
            // ���ۂ̃R���C�_�[���a��`��
            Gizmos.DrawWireSphere(transform.position, col.radius);
        }
        else
        {
            // �G�f�B�^��ŏ����\��
            Gizmos.DrawWireSphere(transform.position, maxRadius);
        }
    }
}
