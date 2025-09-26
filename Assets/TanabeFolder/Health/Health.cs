using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float max_health = 100.0f;
    [SerializeField]
    private float current_health = 100.0f;
    [SerializeField]
    private bool is_invincible = false;
    [SerializeField]
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�_���[�W��^����
    public void TakeDamage(float damage)
    {
        if (is_invincible == false)
        {
            current_health -= damage;
            audio.Play();
        }
    }

    //�񕜂��󂯂�
    public void TakeHeel(float heel)
    {
        current_health += heel;
    }

    //���G�ݒ�
    public void SetInvincible(bool invincible)
    {
        is_invincible = invincible;
    }

    public float GetCurrentHealth() { return current_health; }
    public float NormalizeHealth() { return current_health/max_health; }
}
