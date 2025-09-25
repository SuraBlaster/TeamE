using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : Weapon
{
    public float radius = 2f;
    public float speed = 2f;
    public float startAngle = 0f;

    private float angle;

    void Start()
    {
        angle = startAngle * Mathf.Deg2Rad;
    }

    void Update()
    {

        if (player == null) return;

        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = player.transform.position + new Vector3(x, y, 0);
    }

    protected override void Fire()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log($"{collision.name} に {baseDamage} ダメージ！（Orbit）");
        }
    }
}