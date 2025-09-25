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
            // エフェクトを生成
            GameObject effectInstance = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

            // ここに再生コマンドを追加
            ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                // パーティクルシステムを手動で再生
                ps.Play();

                // 再生時間後に消す
                Destroy(effectInstance, ps.main.duration);
            }
            else
            {
                // ParticleSystemコンポーネントがない場合のフォールバック
                Destroy(effectInstance, 2f);
            }
        }

        Destroy(gameObject);
    }
}