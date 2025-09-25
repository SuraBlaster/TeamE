using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 敵の種類ごとにプレハブと生成位置を格納する
    [Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public float spawnWeight; // 生成確率の重み
    }

    public List<EnemyType> enemyTypes;
    public Transform player_transform;

    [SerializeField]
    private Vector2 spawn_range_max = Vector2.zero;
    [SerializeField]
    private Vector2 spawn_range_min = Vector2.zero;

    float current_time = 0.0f;
    [SerializeField]
    float seconds = 0.5f;

    void Start()
    {
        // 一定時間ごとに敵を生成
        InvokeRepeating("SpawnRandomEnemy", 3f, 3f);
    }

    void Update()
    {
        current_time += Time.deltaTime;
        if (current_time > seconds)
        {
            SpawnRandomEnemy();
            current_time = 0.0f;
        }
    }

    private void SpawnRandomEnemy()
    {
        // ランダムな敵の種類を選択
        EnemyType selectedEnemy = SelectRandomEnemyType();
        if (selectedEnemy != null)
        {
            // 選択された敵を生成
            GameObject newEnemy = Instantiate(selectedEnemy.enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);

            // 基底クラスを使って、すべての敵に共通の振る舞いを指示
            EnemyBase enemyComponent = newEnemy.GetComponent<EnemyBase>();
            if (enemyComponent != null)
            {
                enemyComponent.SetManager(this);
                enemyComponent.SetPlayerTransform(player_transform);
            }
        }
        
    }

    //重み付きで敵をランダム生成
    private EnemyType SelectRandomEnemyType()
    {
        float totalWeight = 0;
        foreach (var type in enemyTypes)
        {
            totalWeight += type.spawnWeight;
        }
        
        float randomValue = UnityEngine.Random.Range(0.0f, totalWeight);
        float currentWeight = 0;

        foreach (var type in enemyTypes)
        {
            currentWeight += type.spawnWeight;
            if (randomValue < currentWeight)
            {
                return type;
            }
        }
        return null;
    }

    //ランダム位置取得
    private Vector3 GetRandomSpawnPosition()
    {
        // プレイヤーから(spawn_range_x,spawn_range_y)離れた位置に生成
        return new Vector3(
            player_transform.position.x + UnityEngine.Random.Range(spawn_range_min.x, spawn_range_max.y),
            player_transform.position.y + UnityEngine.Random.Range(spawn_range_min.x, spawn_range_min.y),
            0);
    }
}
