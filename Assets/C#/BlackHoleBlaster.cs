using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BlackHoleBlaster : Weapon
{
    public GameObject projectilePrefab; 
    public float speed = 5f;

    protected override void Fire()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy == null) return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // 進行方向
        Vector2 dir = (nearestEnemy.transform.position - transform.position).normalized;
        proj.GetComponent<Rigidbody2D>().velocity = dir * speed;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
        proj.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ダメージ設定
        BlackHoleShot p = proj.GetComponent<BlackHoleShot>();
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