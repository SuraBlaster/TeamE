using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField]
    private Transform player_transform;
    [SerializeField]
    private Health health;
    [SerializeField]
    private GameObject death_effect;

    [SerializeField]
    protected float move_speed = 5.0f;

    [SerializeField]
    private float damage = 5.0f;
    [SerializeField]
    int score_num = 10;

    public ScoreScript score;

    [SerializeField]
    private SpriteRenderer sprite;

    bool damaged = false;
    float damage_timer = 0.5f;
    float current_timer = 0.0f;
    float current_change_timer = 0.0f;
    bool color_changer = false;
    float old_health = 0.0f;
  public  void SetOldHealth(float old) { old_health = old; }

    public virtual string GetName() { return "Default"; }

    // Start is called before the first frame update
    void Start()
    {
        old_health = health.GetCurrentHealth();
    }

    protected void Death()
    {
        if(health.GetCurrentHealth() != old_health)
        {
            damaged = true;
        }
        old_health = health.GetCurrentHealth();

        if(damaged)
        {
            current_timer += Time.deltaTime;
            current_change_timer += Time.deltaTime;

            if(current_change_timer>0.1f)
            {
                if(color_changer)
                {
                    sprite.color = Color.white;
                }
                else
                {
                    sprite.color = Color.red;
                }
                color_changer = !color_changer;
                current_change_timer = 0.0f;
            }
            if (current_timer > damage_timer)
            {
                color_changer = false;
                sprite.color = Color.white;
                damaged = false;
                current_timer = 0.0f;
                current_change_timer = 0.0f;
            }
        }

        if (health.GetCurrentHealth() <= 0)
        {
           
            if (death_effect != null)
            {
                GameObject effect = Instantiate(death_effect, transform.position, Quaternion.identity);
                effect.GetComponent<ParticleSystem>().Play();
            }
                Destroy(gameObject);
            manager.AddCount(GetName());
            score.score += score_num;
        }
    }

    protected void LeftorRight(float vec)
    {
        Vector3 scale = transform.localScale;
        if (vec < 0) transform.localScale = new Vector3(MathF.Abs(scale.x) , scale.y, scale.z);
        if (vec >= 0) transform.localScale = new Vector3(MathF.Abs(scale.x)* -1.0f, scale.y, scale.z);
    }

    protected Vector2 ToPlayer() { return player_transform.position - transform.position; }
    public void SetPlayerTransform(Transform transform) { player_transform = transform; }
    public void SetManager(EnemyManager manager) { this.manager = manager; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突した相手がプレイヤーの弾であるかタグで判別
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
