////using UnityEngine;
////using UnityEngine.SceneManagement; // �V�[���Ǘ��̂��߂ɕK�v

////public class KeySceneChanger : MonoBehaviour
////{
////    // �J�ڐ�̃V�[������Inspector�Őݒ�ł���悤�ɂ��܂�
////    public string targetSceneName = "GameScene";

////    void Update()
////    {
////        // ����̃L�[�i�����ł�Space�L�[�j�������ꂽ�u�Ԃ����o
////        if (Input.GetKeyDown(KeyCode.Space))
////        {
////            // �ݒ肳�ꂽ�V�[�����ɑJ��
////            SceneManager.LoadScene(targetSceneName);
////        }
////    }
////}

//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class UniversalInputSceneChanger : MonoBehaviour
//{
//    // �J�ڐ�̃V�[������Inspector�Őݒ�
//    public string targetSceneName = "GameScene";

//    void Update()
//    {
//        // --- (1) �L�[�{�[�h�̔C�ӂ̃L�[�����o ---
//        // �ǂ̃L�[�ł������ꂽ�u�Ԃ� true �ɂȂ�܂�
//        bool keyPressed = Input.anyKeyDown;

//        // --- (2) �}�E�X�̍��N���b�N�����o ---
//        // �}�E�X�̍��{�^���������ꂽ�u�Ԃ� true �ɂȂ�܂� (0 = ���{�^��)
//        bool mouseClicked = Input.GetMouseButtonDown(0);

//        // �ǂ��炩�̓��͂��������ꍇ�A�V�[���J�ڂ����s
//        if (keyPressed || mouseClicked)
//        {
//            LoadTargetScene();
//        }
//    }

//    // �V�[���J�ڂ����s���郁�\�b�h
//    void LoadTargetScene()
//    {
//        // �J�ڐ�̃V�[�������ݒ肳��Ă��邩�m�F
//        if (!string.IsNullOrEmpty(targetSceneName))
//        {
//            SceneManager.LoadScene(targetSceneName);
//        }
//        else
//        {
//            Debug.LogError("�J�ڐ�̃V�[�������ݒ肳��Ă��܂���BInspector���m�F���Ă��������B");
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;           // Image�R���|�[�l���g���g�����߂ɕK�v
using UnityEngine.SceneManagement;
using System.Collections;       // Coroutine���g�����߂ɕK�v

public class UniversalInputSceneChanger : MonoBehaviour
{
    // �J�ڐ�̃V�[������Inspector�Őݒ�
    public string targetSceneName = "GameScene";

    // Inspector�ŁuNext Scene�v�̎q��Image�R���|�[�l���g��ݒ�
    public Image fadeImage;

    // �t�F�[�h�A�E�g�ɂ����鎞�ԁi�b�j
    public float fadeDuration = 0.5f;

    void Update()
    {
        // ���ɑJ�ڏ������n�܂��Ă���ꍇ�́A���͂𖳎�
        if (fadeImage.color.a >= 1f) return;

        // �L�[�{�[�h�̔C�ӂ̃L�[�A�܂��̓}�E�X�̍��N���b�N�����o
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            // ���͂����o���ꂽ��A�R���[�`�����J�n���ăt�F�[�h�A�E�g�ƃV�[���J�ڂ����s
            StartCoroutine(FadeAndLoadScene());
        }
    }

    // �t�F�[�h�A�E�g�ƃV�[���J�ڂ��������s����R���[�`��
    private IEnumerator FadeAndLoadScene()
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image���ݒ肳��Ă��܂���BInspector���m�F���Ă��������B");
            // �摜���Ȃ��ꍇ�͂����ɑJ��
            SceneManager.LoadScene(targetSceneName);
            yield break; // �R���[�`�����I��
        }

        // �����J���[�i���݂̓����x�j���擾
        Color currentColor = fadeImage.color;
        float startTime = Time.time;

        // �t�F�[�h�A�E�g�����i�����x��0����1�֑����j
        while (Time.time < startTime + fadeDuration)
        {
            // �o�ߎ��ԂɊ�Â���0.0����1.0�̒l���擾
            float t = (Time.time - startTime) / fadeDuration;

            // �A���t�@�l�� lerp�i���`��ԁj�Ŋ��炩�ɕω�������
            currentColor.a = Mathf.Lerp(0f, 1f, t);
            fadeImage.color = currentColor;

            yield return null; // 1�t���[���ҋ@
        }

        // ���S�ɕs�����ɂȂ������Ƃ�ۏ�
        currentColor.a = 1f;
        fadeImage.color = currentColor;

        // �V�[���J�ڂ����s
        LoadTargetScene();
    }

    // �V�[���J�ڂ����s���郁�\�b�h
    private void LoadTargetScene()
    {
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