using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class OrbitWeapon : Weapon 
{
    public float radius = 2f;
    public float speed = 2f;
    public float startAngle = 0f; 
    private float angle; void Start() 
    { 
        angle = startAngle * Mathf.Deg2Rad;
    } 
    void Update() 
    { 
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector3 newPos =  transform.parent.position + new Vector3(x, y, 0);
        transform.position = newPos; 
        // プレイヤーから外向きの方向を計算
        Vector2 dir = (newPos - (Vector3)transform.parent.position).normalized;
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 45f;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ); 
    } 
    protected override void Fire() { } 


    private void OnTriggerEnter2D(Collider2D other)
    {
        Health enemy = other.gameObject.GetComponent<Health>();
        if (enemy != null)
        {
            enemy.TakeDamage(baseDamage);
        }
    }
}