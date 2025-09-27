using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestBase : MonoBehaviour
{
    //マネージャー
    QuestManager manager;

    //目標数
    [SerializeField]
    int goal_count;

    //対象
    [SerializeField]
    string enemy_name;

    //クエストのリミット時間
    [SerializeField]
    float limit_timer = 30.0f;
    //現在時間
    float current_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ClearChecker();
    }

    protected void ClearChecker()
    {
        current_time += Time.deltaTime;
        bool finalize = false;
        
        //目標数達成時、成功を返す
        if (manager.GetCount(enemy_name) >= goal_count)
        {
            manager.SetQuestState(QuestManager.QuestState.Suceeded);
            finalize = true;

            SceneManager.LoadScene("GameClear");
        }

        //時間オーバー時、失敗を返す
        if (current_time > limit_timer)
        {
            manager.SetQuestState(QuestManager.QuestState.Failed);
            finalize = true;

            SceneManager.LoadScene("GameOver");
        }

        //削除
        if (finalize) {Destroy(gameObject); }
    }
    
    protected QuestManager GetQuestManager() {  return manager; }
    public void SetQuestManager(QuestManager manager) { this.manager = manager; }

    public float GetCurrentTime() { return current_time; }
    public float GetLimitedTime() { return limit_timer; }
    public float GetNormalizeTime() { return current_time / limit_timer; }
    public int GetGoalCount() { return goal_count; }
    public string GetName() { return enemy_name; }
}
