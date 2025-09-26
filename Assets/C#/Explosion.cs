using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 0.5f;
    public float maxScale = 3f;

    private float timer = 0f;

    void Update()
    {
        // Šg‘å
        timer += Time.deltaTime;
        float t = timer / lifetime;
        float scale = Mathf.Lerp(0.1f, maxScale, t);
        transform.localScale = new Vector3(scale, scale, 1f);

        // Žõ–½‚ÅÁ‚¦‚é
        if (timer >= lifetime)
        {
            Destroy(gameObject);  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemy = other.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
