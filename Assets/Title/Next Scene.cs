//using UnityEngine;
//using UnityEngine.SceneManagement; // �V�[���Ǘ��̂��߂ɕK�v

//public class KeySceneChanger : MonoBehaviour
//{
//    // �J�ڐ�̃V�[������Inspector�Őݒ�ł���悤�ɂ��܂�
//    public string targetSceneName = "GameScene";

//    void Update()
//    {
//        // ����̃L�[�i�����ł�Space�L�[�j�������ꂽ�u�Ԃ����o
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            // �ݒ肳�ꂽ�V�[�����ɑJ��
//            SceneManager.LoadScene(targetSceneName);
//        }
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalInputSceneChanger : MonoBehaviour
{
    // �J�ڐ�̃V�[������Inspector�Őݒ�
    public string targetSceneName = "GameScene";

    void Update()
    {
        // --- (1) �L�[�{�[�h�̔C�ӂ̃L�[�����o ---
        // �ǂ̃L�[�ł������ꂽ�u�Ԃ� true �ɂȂ�܂�
        bool keyPressed = Input.anyKeyDown;

        // --- (2) �}�E�X�̍��N���b�N�����o ---
        // �}�E�X�̍��{�^���������ꂽ�u�Ԃ� true �ɂȂ�܂� (0 = ���{�^��)
        bool mouseClicked = Input.GetMouseButtonDown(0);

        // �ǂ��炩�̓��͂��������ꍇ�A�V�[���J�ڂ����s
        if (keyPressed || mouseClicked)
        {
            LoadTargetScene();
        }
    }

    // �V�[���J�ڂ����s���郁�\�b�h
    void LoadTargetScene()
    {
        // �J�ڐ�̃V�[�������ݒ肳��Ă��邩�m�F
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("�J�ڐ�̃V�[�������ݒ肳��Ă��܂���BInspector���m�F���Ă��������B");
        }
    }
}