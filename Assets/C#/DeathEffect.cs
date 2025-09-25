using UnityEngine;

public class DeathByBool : MonoBehaviour
{
    public bool isDead = false;
    public GameObject deathEffectPrefab;
    private bool hasDied = false;

    void Update()
    {
        if (isDead && !hasDied)
        {
            Die();
        }
    }

    void Die()
    {
        hasDied = true;

        if (deathEffectPrefab != null)
        {
            // �G�t�F�N�g�𐶐�
            GameObject effectInstance = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

            // �����ɍĐ��R�}���h��ǉ�
            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // �p�[�e�B�N���V�X�e�����蓮�ōĐ�
                ps.Play();

                // �Đ����Ԍ�ɏ���
                Destroy(effectInstance, ps.main.duration);
            }
            else
            {
                // ParticleSystem�R���|�[�l���g���Ȃ��ꍇ�̃t�H�[���o�b�N
                Destroy(effectInstance, 2f);
            }
        }

        Destroy(gameObject);
    }
}