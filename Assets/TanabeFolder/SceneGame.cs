using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyManager;

public class SceneGame : MonoBehaviour
{
    public EnemyManager enemyManager;
    public TimerUIScript timer_ui;

    // �G�̎�ނ��ƂɃv���n�u�Ɛ����ʒu���i�[����
    [Serializable]
    public class Wave
    {
        public float time;
        public float hp_correction;
        public float spawn_seconds;
        public int spawn_amonut;
    }
    public List<Wave> waves;
    int conut = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wave wave = waves[conut];
        //�����ݒ�
        enemyManager.SetSeconds(wave.spawn_seconds);
        enemyManager.SetAmount(wave.spawn_amonut);
        enemyManager.CorrectionValue(wave.hp_correction);

        //���ԊǗ�
        if (wave.time < timer_ui.NormalizeTime()) conut++;
        if (conut >= waves.Count) conut = waves.Count;
    }
}
