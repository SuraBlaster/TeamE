using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    // �G�̎�ނ��ƂɃv���n�u�Ɛ����ʒu���i�[����
    [Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public float spawnWeight; // �����m���̏d��
    }

    public List<EnemyType> enemyTypes;
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

    [SerializeField]
    ScoreScript score;
    void Start()
    {
        enemy_count = new Dictionary<string, int>();

        // ��莞�Ԃ��ƂɓG�𐶐�
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
        // �����_���ȓG�̎�ނ�I��
        EnemyType selectedEnemy = SelectRandomEnemyType();
        if (selectedEnemy != null)
        {
            // �I�����ꂽ�G�𐶐�
            GameObject newEnemy = Instantiate(selectedEnemy.enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity, transform);

            // ���N���X���g���āA���ׂĂ̓G�ɋ��ʂ̐U�镑�����w��
            EnemyBase enemyComponent = newEnemy.GetComponent<EnemyBase>();
            if (enemyComponent != null)
            {
                enemyComponent.SetManager(this);
                enemyComponent.SetPlayerTransform(player_transform);

                enemyComponent.SetOldHealth(enemyComponent.GetComponent<Health>().GetCurrentHealth());
                enemyComponent.slotsParent = slots_transform;
                enemyComponent.score = score;
            }            
        }
        
    }

    //�d�ݕt���œG�������_������
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

    //�����_���ʒu�擾
    private Vector3 GetRandomSpawnPosition()
    {
        // �v���C���[����(spawn_range_x,spawn_range_y)���ꂽ�ʒu�ɐ���
        return new Vector3(
            player_transform.position.x + UnityEngine.Random.Range(spawn_range_min.x, spawn_range_max.y),
            player_transform.position.y + UnityEngine.Random.Range(spawn_range_min.x, spawn_range_max.y),
            0);
    }

    //���S�J�E���g
    public void AddCount(string name)
    {
        if (enemy_count.ContainsKey(name))
        {
            // �L�[�����݂���ꍇ�A�l��1���₷
            enemy_count[name]++;
        }
        else
        {
            // �L�[�����݂��Ȃ��ꍇ�A�V�����ǉ�����
            enemy_count.Add(name, 1);
        }
    }

    //���S���擾
    public int GetCount(string name) 
    {
        if (enemy_count == null) return 0;
        // �L�[�����݂��邩�m�F
        if (enemy_count.ContainsKey(name))
        {
            // ���݂���ꍇ�͒l��Ԃ�
            return enemy_count[name];
        }
        // ���݂��Ȃ��ꍇ��0��Ԃ�
        return 0;
    }

    public void ClearCount() { if(enemy_count!=null)enemy_count.Clear(); }
}
