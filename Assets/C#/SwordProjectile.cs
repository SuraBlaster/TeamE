using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SwordProjectile2D : Weapon
{
    public GameObject projectilePrefab; // ��΂�����Prefab
    public float speed = 5f;

    protected override void Fire()
    {
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy == null) return;

        // ���𐶐�
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // ��΂�����
        Vector2 dir = (nearestEnemy.transform.position - transform.position).normalized;
        proj.GetComponent<Rigidbody2D>().velocity = dir * speed;

        // �_���[�W�ݒ��Projectile�֓n��
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