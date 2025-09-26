using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class AxeProjectile : Projectile
{
    public GameObject shockwavePrefab;   // 衝撃波のプレハブ
    public float shockwaveRadius = 2f;   // 衝撃波の半径
    public LayerMask enemyLayer;         // 敵のレイヤー判定用

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
        // 斧が地面や敵に着弾したとき
        Explode();
    }

    private void Explode()
    {
        // 衝撃波の生成（アニメーション付きPrefab）
        if (shockwavePrefab != null)
        {
            Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
        }

        // 範囲内の敵を検索
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius, enemyLayer);
        foreach (var hit in hits)
        {
            Health enemy = hit.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Projectileから継承したdamage
            }
        }

        Destroy(gameObject); // 斧本体は消える
    }
}