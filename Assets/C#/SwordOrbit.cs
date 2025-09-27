using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UIElements;

public class OrbitWeapon : Weapon 
{
    public float radius = 2f;
    public float speed = 2f;
    public float startAngle = 0f;
    public float impalse = 1.0f;
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
        float rotationZ;
        if (transform.parent.localScale.x < 0)
        {
            rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 135;
        }
        else
        {
            rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 45;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, rotationZ); 
    } 
    protected override void Fire() { } 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Health enemy = other.gameObject.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(baseDamage);

            }
            Vector2 direction = (-transform.position + other.transform.position).normalized;

            other.attachedRigidbody.AddForce(direction * impalse);
        }
        
    }
}