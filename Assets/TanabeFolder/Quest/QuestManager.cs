using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyManager;
using static QuestManager;

public class QuestManager : MonoBehaviour
{
    // �N�G�X�g�̎�ނ��ƂɃv���n�u�Ɛ����ʒu���i�[����
    [Serializable]
    public class QuestType
    {
        public GameObject quest_prefub;
        public float spawnWeight; // �����m���̏d��
    }
    public List<QuestType> questTypes;

    [SerializeField]
    EnemyManager enemy_manager;

    //���݃N�G�X�g
    QuestBase current_quest;

    //�N�G�X�g���
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

    // �d�ݕt���ŃN�G�X�g�������_������
    private QuestType SelectRandomQuestType()
    {
        float totalWeight = 0;
        // questTypes���X�g�𔽕�����
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
