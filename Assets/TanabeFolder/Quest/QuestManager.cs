using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyManager;
using static QuestManager;

public class QuestManager : MonoBehaviour
{
    // クエストの種類ごとにプレハブと生成位置を格納する
    [Serializable]
    public class QuestType
    {
        public GameObject quest_prefub;
        public float spawnWeight; // 生成確率の重み
    }
    public List<QuestType> questTypes;

    [SerializeField]
    EnemyManager enemy_manager;

    //現在クエスト
    QuestBase current_quest;

    //クエスト状態
    public enum QuestState
    {
        None,
        Running,
        Failed,
        Suceeded,
    }
    QuestState questState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuest()
    {
        enemy_manager.ClearCount();
        questState = QuestState.Running;
        current_quest = SelectRandomQuestType().quest_prefub.GetComponent<QuestBase>();
        current_quest.SetQuestManager(this);
    }

    // 重み付きでクエストをランダム生成
    private QuestType SelectRandomQuestType()
    {
        float totalWeight = 0;
        // questTypesリストを反復処理
        foreach (var type in questTypes)
        {
            totalWeight += type.spawnWeight;
        }

        float randomValue = UnityEngine.Random.Range(0.0f, totalWeight);
        float currentWeight = 0;

        foreach (var type in questTypes)
        {
            currentWeight += type.spawnWeight;
            if (randomValue < currentWeight)
            {
                return type;
            }
        }
        return null;
    }
    public QuestBase GetQuest() { return current_quest; }
    public QuestState GetQuestState() { return questState; }
    public void SetQuestState(QuestState state) { questState = state; }
    public int GetCount(string name) { return enemy_manager.GetCount(name); }
}
