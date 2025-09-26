using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static EnemyManager;
using static QuestManager;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    float start_time = 10.0f;
    float current_time;

    // クエストの種類ごとにプレハブと生成位置を格納する
    [Serializable]
    public class QuestType
    {
        public GameObject quest_prefub;
        public float spawnWeight; // 生成確率の重み
    }
    public List<QuestType> questTypes;

    [SerializeField]
    GameObject eneable_object;

    [SerializeField]
    EnemyManager enemy_manager;
    [SerializeField]
    GameObject mouseUi;
    [SerializeField]
    GameObject spiderUi;
    [SerializeField]
    GameObject frogUi;


    //現在クエスト
    QuestBase current_quest;

    //タイマーUI
    [SerializeField]
    UnityEngine.UI.Image timer_image;
    [SerializeField]
    Text timer_text;


    //クエストUI
    [SerializeField]
    Text current_count;
    [SerializeField]
    Text goal_count;

    //クエスト状態
    public enum QuestState
    {
        None,
        Running,
        Failed,
        Suceeded,
    }
    QuestState questState=QuestState.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current_time += Time.deltaTime;
        if (start_time < current_time && questState == QuestState.None)
        {
            StartQuest();
        }

        //UI操作
        if (current_quest != null)
        {
            eneable_object.SetActive(true);

            //タイマー系
            {
                timer_image.fillAmount = 1.0f - current_quest.GetNormalizeTime();
                int num = Mathf.FloorToInt(current_quest.GetLimitedTime() - current_quest.GetCurrentTime());
                if (num < 10)
                    timer_text.text = string.Format(" {0}", num);
                else
                    timer_text.text = string.Format("{0}", num);
            }

            //クエスト系
            {
                string name = current_quest.GetName();
                if (name == "Flog") frogUi.SetActive(true);
                if (name == "Spider") spiderUi.SetActive(true);
                if (name == "Mouse") mouseUi.SetActive(true);

                int goalnum = current_quest.GetGoalCount();
                if (goalnum < 10)
                    goal_count.text = string.Format(" {0}", goalnum);
                else
                    goal_count.text = string.Format("{0}", goalnum);
                int currentnum = enemy_manager.GetCount(name);
                if (currentnum < 10)
                    current_count.text = string.Format(" {0}", Mathf.FloorToInt(currentnum));
                else
                    current_count.text = string.Format("{0}", Mathf.FloorToInt(currentnum));
            }
        }
        else
        {
            eneable_object.SetActive(false);
            frogUi.SetActive(false);
            spiderUi.SetActive(false);
            mouseUi.SetActive(false);
        }
    }

    public void StartQuest()
    {
        enemy_manager.ClearCount();
        questState = QuestState.Running;
        GameObject newQuest = Instantiate(SelectRandomQuestType().quest_prefub, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, transform);
        current_quest = newQuest.GetComponent<QuestBase>();
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
