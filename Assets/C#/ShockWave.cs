using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 0.5f;
    public float maxRadius = 3f;

    private CircleCollider2D col;
    private float timer = 0f;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;
        col.radius = Mathf.Lerp(0f, maxRadius, t); // éûä‘Ç…çáÇÌÇπÇƒçLÇ™ÇÈ
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health enemy = collision.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
