using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    public GameObject axePrefab; // �����镀�̃v���n�u
    public float throwForce = 10f;

    protected override void Fire()
    {
        if (player == null || axePrefab == null) return;

        // �����_�������i360�x�j
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // ���𐶐�
        GameObject axe = Instantiate(axePrefab, player.transform.position, Quaternion.identity);

        // Projectile ������
        Rigidbody2D rb = axe.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(dir * throwForce, ForceMode2D.Impulse);
        }

        // ���Ƀ_���[�W�l��n��
        AxeProjectile axeProj = axe.GetComponent<AxeProjectile>();
        if (axeProj != null)
        {
            axeProj.damage = baseDamage;
        }
    }
}
