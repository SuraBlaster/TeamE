using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SwordProjectile2D : Weapon
{
    public GameObject projectilePrefab; // 飛ばす剣のPrefab
    public float speed = 5f;

    protected override void Fire()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy == null) return;

        Vector3 spawnPos = player.transform.position;

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        // 進行方向
        Vector2 dir = (nearestEnemy.transform.position - spawnPos).normalized;
        proj.GetComponent<Rigidbody2D>().velocity = dir * speed;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 45f;
        proj.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ダメージ設定
        Projectile p = proj.GetComponent<Projectile>();
        if (p != null) p.damage = baseDamage;
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }
}