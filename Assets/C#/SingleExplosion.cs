using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleExplosion : MonoBehaviour
{
    public GameObject prefab;         // Explosion Prefab
    public float spawnInterval = 2f;

    private float spawnTimer = 0f;

    void Start()
    {
        SpawnOne();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnOne();
            spawnTimer = 0f;
        }
    }

    private void SpawnOne()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
