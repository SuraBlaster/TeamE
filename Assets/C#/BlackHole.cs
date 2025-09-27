using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float radius = 5f;           // ���ʔ͈�
    public float pullForce = 50f;       // �z����
    public float damagePerSecond = 10f; // �b�ԃ_���[�W
    public float duration = 5f;         // ��������
    public GameObject explosionPrefab;  // �Ō�ɏo��Explosion

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Explode();
            return;
        }



        // �͈͓��̑S�R���C�_�[���擾
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hit in hits)
        {
            //  Enemy �^�O�ȊO�͊��S�X�L�b�v
            if (!hit.CompareTag("Enemy")) continue;

            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb != null)
            {
                // �����񂹂����
                Vector2 dir = (transform.position - hit.transform.position).normalized;
                rb.AddForce(dir * pullForce);

                // �_���[�W
                Health enemy = hit.GetComponent<Health>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }
    }


    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}