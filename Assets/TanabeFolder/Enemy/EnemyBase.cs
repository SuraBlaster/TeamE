using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public virtual string GetName() { return "Default"; }

    // Start is called before the first frame update
    void Start()
    {
    }

    protected void Death()
    {
        if (health.GetCurrentHealth() <= 0)
        {
           
            if (death_effect != null)
            {
                GameObject effect = Instantiate(death_effect, transform.position, Quaternion.identity);
                effect.GetComponent<ParticleSystem>().Play();
            }
                Destroy(gameObject);
            manager.AddCount(GetName());
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
}
