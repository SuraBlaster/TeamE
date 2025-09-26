using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xplosion : MonoBehaviour
{
    public GameObject explosionPrefab; // © Explosion‚ÌPrefab‚ð“ü‚ê‚é
    public float offset = 1.5f;        // ŽÎ‚ß‚Ì‹——£
    public float spawnInterval = 2f;   // ‰½•b‚²‚Æ‚É”­¶‚·‚é‚©

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnX();
            spawnTimer = 0f;
        }
    }

    void SpawnX()
    {
        // ’†‰›
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // ŽÎ‚ß4•ûŒü
        Vector2[] dirs = {
            new Vector2(1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(-1, -1).normalized
        };

        foreach (var dir in dirs)
        {
            Vector3 pos = transform.position + (Vector3)(dir * offset);
            Instantiate(explosionPrefab, pos, Quaternion.identity);
        }
    }
}