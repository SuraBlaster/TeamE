using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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


    public List<GameObject> prefabWeapons;


    public Transform player_transform;
    public Transform slots_transform;

    Dictionary<string, int> enemy_count;

    [SerializeField]
    private Vector2 spawn_range_max = Vector2.zero;
    [SerializeField]
    private Vector2 spawn_range_min = Vector2.zero;

    float current_time = 0.0f;
    [SerializeField]
    float seconds = 0.5f;
    public void SetSeconds(float seconds) { this.seconds = seconds; }
    [SerializeField]
    int spown_amount = 5;
    public void SetAmount(int amount) { spown_amount = amount; }
    //補正値
    float correction_value = 1.0f;
    public void CorrectionValue(float amount) { correction_value = amount; }

    [SerializeField]
    //ScoreScript score;
    void Start()
    {
        enemy_count = new Dictionary<string, int>();

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
        for (int count = 0; count < spown_amount; count++)
        {

            // ランダムな敵の種類を選択
            EnemyType selectedEnemy = SelectRandomEnemyType();
            if (selectedEnemy != null)
            {
                // 選択された敵を生成
                GameObject newEnemy = Instantiate(selectedEnemy.enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity, transform);

                // 基底クラスを使って、すべての敵に共通の振る舞いを指示
                EnemyBase enemyComponent = newEnemy.GetComponent<EnemyBase>();
                if (enemyComponent != null)
                {
                    enemyComponent.SetManager(this);
                    enemyComponent.SetPlayerTransform(player_transform);

                    //HP設定
                    Health enemy_health = enemyComponent.GetComponent<Health>();
                    enemy_health.SetMaxHealth(enemy_health.GetMaxHealth() * correction_value);
                    enemyComponent.SetOldHealth(enemy_health.GetCurrentHealth());

                    //武器リスト渡す
                    enemyComponent.prefabWeapons = prefabWeapons;

                    enemyComponent.slotsParent = slots_transform;
                    //enemyComponent.score = score;
                }
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
        Vector3 spawnPosition = transform.position;
        // プレイヤーから(spawn_range_x,spawn_range_y)離れた位置に生成
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                spawnPosition= new Vector3(
                    player_transform.position.x + spawn_range_min.x,
                    player_transform.position.y + UnityEngine.Random.Range(spawn_range_min.y, spawn_range_max.y),
                    0);
                break;
            case 1:
                spawnPosition= new Vector3(
                    player_transform.position.x + spawn_range_max.x,
                    player_transform.position.y + UnityEngine.Random.Range(spawn_range_min.y, spawn_range_max.y),
                    0);
                break;
            case 2:
                spawnPosition= new Vector3(
                    player_transform.position.x + UnityEngine.Random.Range(spawn_range_min.x, spawn_range_max.x),
                    player_transform.position.y + spawn_range_min.y,
                    0);
                break;
            case 3:
                spawnPosition = new Vector3(
                    player_transform.position.x + UnityEngine.Random.Range(spawn_range_min.x, spawn_range_max.x),
                    player_transform.position.y + spawn_range_max.y,
                    0);
                break;
            default:
                int i = 0;
                break;
        }

        return spawnPosition;
    }

    //死亡カウント
    public void AddCount(string name)
    {
        if (enemy_count.ContainsKey(name))
        {
            // キーが存在する場合、値を1増やす
            enemy_count[name]++;
        }
        else
        {
            // キーが存在しない場合、新しく追加する
            enemy_count.Add(name, 1);
        }
    }

    //死亡数取得
    public int GetCount(string name) 
    {
        if (enemy_count == null) return 0;
        // キーが存在するか確認
        if (enemy_count.ContainsKey(name))
        {
            // 存在する場合は値を返す
            return enemy_count[name];
        }
        // 存在しない場合は0を返す
        return 0;
    }

    public void ClearCount() { if(enemy_count!=null)enemy_count.Clear(); }
}
