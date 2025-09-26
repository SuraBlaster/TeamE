using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shot : Weapon
{
    public GameObject projectilePrefab; 
    public float speed = 5f;

    protected override void Fire()
    {
        

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // 進行方向
        Vector2 dir;
        Vector2 scale = proj.transform.localScale;
        if (transform.parent.localScale.x < 0)
        {
            dir = new Vector2(-1, 0);
            scale.x *= 1;
        }
        else
        {
            dir = new Vector2(1, 0);
            scale.x *= -1;
        }

        proj.GetComponent<Rigidbody2D>().velocity = dir * speed;


        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        proj.transform.localScale = scale;

        // ダメージ設定
        SwordShot p = proj.GetComponent<SwordShot>();
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