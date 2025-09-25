using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public PlayerController player; // �v���C���[�Q��
    public float baseDamage = 10f;  // ��b�_���[�W��
    public float fireRate = 1f;     // ���ˊԊu(�b)

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

    // �h���N���X���Ƃ̍U������
    protected abstract void Fire();
}
