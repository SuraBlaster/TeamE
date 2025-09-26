using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : Projectile
{
    public float throwForce = 10f;          // 投げる力
    public float shockwaveRadius = 3f;      // 衝撃波の範囲
    public GameObject shockwavePrefab;      // 衝撃波のエフェクトPrefab

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 一定時間後に消える処理は基底ProjectileのStart()で動作
        base.OnProjectileStart();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 範囲内の敵にダメージ
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy")) // Tagで判定
            {
                Health enemy = hit.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                // 衝撃波を出す
                if (shockwavePrefab != null)
                {
                    Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
                }
                Destroy(gameObject); // 斧本体は消える
            }
        }

        
    }
}