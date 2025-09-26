using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int baseDamage = 10;  // ��b�_���[�W��
    public float fireRate = 1f;     // ���ˊԊu(�b)

    private float fireTimer;

    public virtual void Initialize(PlayerController playerController, int damage, float rate)
    {
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

    // �h���N���X���Ƃ̍U������
    protected abstract void Fire();
}
