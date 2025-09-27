using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestBase : MonoBehaviour
{
    //�}�l�[�W���[
    QuestManager manager;

    //�ڕW��
    [SerializeField]
    int goal_count;

    //�Ώ�
    [SerializeField]
    string enemy_name;

    //�N�G�X�g�̃��~�b�g����
    [SerializeField]
    float limit_timer = 30.0f;
    //���ݎ���
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
        
        //�ڕW���B�����A������Ԃ�
        if (manager.GetCount(enemy_name) >= goal_count)
        {
            manager.SetQuestState(QuestManager.QuestState.Suceeded);
            finalize = true;

            SceneManager.LoadScene("GameClear");
        }

        //���ԃI�[�o�[���A���s��Ԃ�
        if (current_time > limit_timer)
        {
            manager.SetQuestState(QuestManager.QuestState.Failed);
            finalize = true;

            SceneManager.LoadScene("GameOver");
        }

        //�폜
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
