using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    public GameObject axePrefab; // 投げる斧のプレハブ
    public float throwForce = 10f;

    protected override void Fire()
    {

        // ランダム方向（90度）
        float angle = Random.Range(45f,135f) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // 斧を生成
        GameObject axe = Instantiate(axePrefab, transform.position, Quaternion.identity);

        // Projectile 初期化
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(dir * throwForce, ForceMode2D.Impulse);
        }

        // 斧にダメージ値を渡す
        AxeProjectile axeProj = axe.GetComponent<AxeProjectile>();
        if (axeProj != null)
        {
            axeProj.damage = baseDamage;
        }
    }
}
