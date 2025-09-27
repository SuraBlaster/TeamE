using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float radius = 5f;           // 効果範囲
    public float pullForce = 50f;       // 吸引力
    public float damagePerSecond = 10f; // 秒間ダメージ
    public float duration = 5f;         // 存続時間
    public GameObject explosionPrefab;  // 最後に出すExplosion

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Explode();
            return;
        }



        // 範囲内の全コライダーを取得
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hit in hits)
        {
            //  Enemy タグ以外は完全スキップ
            if (!hit.CompareTag("Enemy")) continue;

            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb != null)
            {
                // 引き寄せる方向
                Vector2 dir = (transform.position - hit.transform.position).normalized;
                rb.AddForce(dir * pullForce);

                // ダメージ
                Health enemy = hit.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }
    }


    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}