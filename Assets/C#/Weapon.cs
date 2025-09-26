using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public PlayerController player; // プレイヤー参照
    public int baseDamage = 10;  // 基礎ダメージ量
    public float fireRate = 1f;     // 発射間隔(秒)

    private float fireTimer;

    public virtual void Initialize(PlayerController playerController, int damage, float rate)
    {
        player = playerController;
        baseDamage = damage;
        fireRate = rate;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health enemy = collision.gameObject.GetComponent<Health>();
        if (enemy != null)
        {
            enemy.TakeDamage(baseDamage);
        }
    }

    // 派生クラスごとの攻撃処理
    protected abstract void Fire();
}
