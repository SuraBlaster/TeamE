using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float max_health = 100.0f;
    public void SetMaxHealth(float max_health) { this.max_health = max_health; current_health = max_health; }
    public float GetMaxHealth() { return this.max_health; }
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
        if(current_health<=0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    //ƒ_ƒ[ƒW‚ð—^‚¦‚é
    public void TakeDamage(float damage)
    {
        if (is_invincible == false)
        {
            current_health -= damage;
            audio.Play();
        }
    }

    //‰ñ•œ‚ðŽó‚¯‚é
    public void TakeHeel(float heel)
    {
        current_health += heel;
        if(max_health <= current_health) current_health=max_health;
    }

    //–³“GÝ’è
    public void SetInvincible(bool invincible)
    {
        is_invincible = invincible;
    }

    public float GetCurrentHealth() { return current_health; }
    public float NormalizeHealth() { return current_health/max_health; }
}
