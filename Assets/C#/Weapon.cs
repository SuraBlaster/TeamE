using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public PlayerController player; // プレイヤー参照
    public float baseDamage = 10f;  // 基礎ダメージ量
    public float fireRate = 1f;     // 発射間隔(秒)

    private float fireTimer;

    public virtual void Initialize(PlayerController playerController, float damage, float rate)
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

    // 派生クラスごとの攻撃処理
    protected abstract void Fire();
}
