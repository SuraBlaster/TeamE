//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI; // Text���g���ꍇ
//using TMPro; // TextMeshProUGUI���g���ꍇ
//using UnityEngine.SceneManagement; // �� �ǉ�: �V�[���Ǘ��@�\���g�p���邽�߂ɕK�v

//public class CheckboxEffectController_v4 : MonoBehaviour
//{
//    // ===============================================
//    // �� �C���_: Inspector�Ŋm�F�E���삷�邽�߂̃u�[���l
//    // ===============================================
//    [Header("�� Inspector�p ����m�F�X�C�b�`")]
//    [Tooltip("���̃`�F�b�N�{�b�N�X��ON/OFF�ŃG�t�F�N�g�𐧌䂵�܂��B")]
//    public bool IsActivated = false;

//    // �� �ǉ�: �e�X�g���[�h�p�`�F�b�N�{�b�N�X
//    [Tooltip("�e�X�g�p�̃f�o�b�O���O���o�͂��A�G�t�F�N�g��ɃV�[�������[�h���邩�𐧌䂵�܂��B")]
//    public bool IsTestMode = false;

//    // �� �ǉ�: ���[�h����e�X�g�V�[����
//    [Header("�� �e�X�g���[�h�ݒ�")]
//    [Tooltip("IsTestMode��ON�̂Ƃ��AIsActivated��ON�ɂȂ�ƃ��[�h����V�[����")]
//    public string TestSceneName = "TestScene";

//    // ���݂̃u�[���l�̏�Ԃ�ێ����A�ω������o���邽�߂Ɏg�p
//    private bool wasActivated = false;

//    // ===============================================

//    // === 1. �����x�ύX�iSprite Renderer���g�p�j�̐ݒ� ===
//    [Header("1. �����x��ύX����摜 (SpriteRenderer)")]
//    public SpriteRenderer spriteToFade;
//    public float targetAlpha = 0.8f;      // �ύX��̖ڕW�����x (0.0 �` 1.0)
//    public float fadeDuration = 0.5f;      // �t�F�[�h�ɂ����鎞��

//    // === 2. �ʒu�ړ��iTransform���g�p�j�̐ݒ� ===
//    [Header("2. �ړ��ݒ�̋��ʐݒ�")]
//    public float moveDuration = 0.5f;      // �ړ��ɂ����鎞��

//    [Header("2-1. �K�{�̈ړ��摜 (2��)")]
//    public Transform objectToMove2;
//    public float targetHeightY2 = 100f;    // 2�ڂ̖ڕWY���W�i���[���h���W�j
//    private float initialY2;               // 2�ڂ̉摜�̏���Y���W��ۑ�

//    [Header("2-2. �I�v�V�����̈ړ��摜 (3��)")]
//    [Tooltip("���̍��ڂ͋�ł����삵�܂��B")]
//    public Transform objectToMove3;
//    public float targetHeightY3 = 50f;     // 3�ڂ̖ڕWY���W�i���[���h���W�j
//    private float initialY3;               // 3�ڂ̉摜�̏���Y���W��ۑ�


//    // �� �C���E�ǉ�: �X�R�A�\���p�ݒ�
//    [Header("3. �X�R�A�\���̐ݒ�")]
//    public ScoreScript scoreScript;          // ScoreScript�ւ̎Q�� (�X�R�A�l�̎擾�p)
//    [Tooltip("�X�R�A�e�L�X�g��Transform�����蓖�ĂĂ��������B")]
//    public Transform scoreTextTransform;     // �X�R�A�e�L�X�g��Transform (�ړ��E�\������p)
//    private float initialScoreTextY;          // �X�R�A�e�L�X�g�̏���Y���W��ۑ�
//    // �� �⑫: �X�R�A�e�L�X�g�̕\��/��\���Ɏg���R���|�[�l���g (Text��TextMeshProUGUI)
//    private Component scoreTextComponent;

//    // MonoBehaviour��Start���\�b�h�ŏ����ʒu���L�^
//    void Start()
//    {
//        // 2�ڂ̈ړ��I�u�W�F�N�g�̏���Y���W��ۑ� (�K�{)
//        if (objectToMove2 != null)
//        {
//            initialY2 = objectToMove2.position.y;
//        }

//        // 3�ڂ̈ړ��I�u�W�F�N�g�̏���Y���W��ۑ� (�I�v�V����)
//        if (objectToMove3 != null)
//        {
//            initialY3 = objectToMove3.position.y;
//        }

//        // �� �C��: �X�R�A�e�L�X�g�̏���Y���W�ƃR���|�[�l���g��ۑ�
//        if (scoreTextTransform != null)
//        {
//            initialScoreTextY = scoreTextTransform.position.y;

//            // Text�܂���TextMeshProUGUI�R���|�[�l���g���擾���A�\��/��\���ɔ�����
//            scoreTextComponent = scoreTextTransform.GetComponent<Text>();
//            if (scoreTextComponent == null)
//            {
//                scoreTextComponent = scoreTextTransform.GetComponent<TextMeshProUGUI>();
//            }

//            // ������Ԃ��A�N�e�B�u�ɂ���
//            if (scoreTextComponent != null)
//            {
//                scoreTextComponent.gameObject.SetActive(false);
//            }
//        }

//        // ������Ԃ��L�^
//        wasActivated = IsActivated;
//    }

//    // ===============================================
//    // Update�͕ύX�Ȃ�
//    // ===============================================
//    void Update()
//    {
//        // IsActivated �̒l���O�̃t���[������ω������ꍇ�̂ݏ��������s
//        if (IsActivated != wasActivated)
//        {
//            // �A�j���[�V�������������s
//            ExecuteEffects(IsActivated);

//            // ��Ԃ��X�V
//            wasActivated = IsActivated;
//        }
//    }
//    // ===============================================

//    // �� �ǉ�: �S�ẴG�t�F�N�g�̊�����҂��A�Ō�ɃV�[�������[�h����R���[�`��
//    private IEnumerator WaitForEffectsAndLoad(string sceneName)
//    {
//        // 1. �����x�ύX�̊�����҂�
//        if (spriteToFade != null)
//        {
//            yield return StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));
//        }

//        // 2. objectToMove2�̈ړ��̊�����҂�
//        if (objectToMove2 == null)
//        {
//            Debug.LogError("objectToMove2���ݒ肳��Ă��܂���B�ړ��������X�L�b�v���܂��B");
//        }
//        else
//        {
//            yield return StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
//        }

//        // 3. objectToMove3�̈ړ��̊�����҂�
//        if (objectToMove3 != null)
//        {
//            yield return StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
//        }

//        // 4. �X�R�A�e�L�X�g�̏����ƈړ��̊�����҂�
//        if (scoreTextTransform != null && scoreTextComponent != null)
//        {
//            scoreTextComponent.gameObject.SetActive(true);
//            yield return StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));
//        }

//        // �S�ẴG�t�F�N�g������������V�[�������[�h
//        Debug.Log("�e�X�g���[�h: �S�ẴG�t�F�N�g���������܂����B�V�[��: " + sceneName + "�����[�h���܂��B");
//        // **while ���[�v�� yield return null** ���g���āA���͂�����܂őҋ@
//        while (!Input.anyKeyDown)
//        {
//            yield return null; // 1�t���[���ҋ@
//        }
//        // ���͂����o���ꂽ��A�R���[�`�����J�n���ăt�F�[�h�A�E�g�ƃV�[���J�ڂ����s
//        SceneManager.LoadScene(sceneName);

//    }


//    // �� �C��: �e�X�g���[�hON�̏ꍇ�ɃV�[�P���X�R���[�`�����Ăяo��
//    private void ExecuteEffects(bool isChecked)
//    {
//        if (isChecked)
//        {
//            // ===== ON (�`�F�b�N��) �̏��� =====

//            if (IsTestMode)
//            {
//                // IsTestMode��ON�̏ꍇ: �G�t�F�N�g�����ԂɎ��s���A������ɃV�[�������[�h
//                StartCoroutine(WaitForEffectsAndLoad(TestSceneName));
//                return; // �V�[�P���X�R���[�`���ɏ������Ϗ����邽�߁A�����ŏI��
//            }

//            // --- IsTestMode��OFF�̏ꍇ�i�ʏ�̓���: �G�t�F�N�g����s���Ď��s�j ---
//            StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));

//            if (objectToMove2 == null)
//            {
//                Debug.LogError("objectToMove2 (2�ڂ̉摜) ���ݒ肳��Ă��܂���B");
//            }
//            else
//            {
//                StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
//            }

//            if (objectToMove3 != null)
//            {
//                StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
//            }

//            // �X�R�A�e�L�X�g�̏���
//            if (scoreTextTransform != null && scoreTextComponent != null)
//            {
//                scoreTextComponent.gameObject.SetActive(true);
//                StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));
//            }
//        }
//        else
//        {
//            // ===== OFF (�`�F�b�N������) �̏��� (�V�[�����[�h���Ȃ����ߕύX�Ȃ�) =====
//            if (IsTestMode)
//            {
//                Debug.Log("IsActivated��OFF�ɂȂ�܂����B(�e�X�g���[�h)");
//            }

//            StartCoroutine(FadeAlpha(spriteToFade, 1.0f, fadeDuration));

//            if (objectToMove2 != null)
//            {
//                StartCoroutine(MoveToHeight(objectToMove2, initialY2, moveDuration));
//            }

//            if (objectToMove3 != null)
//            {
//                StartCoroutine(MoveToHeight(objectToMove3, initialY3, moveDuration));
//            }

//            // �X�R�A�e�L�X�g�̏���
//            if (scoreTextTransform != null && scoreTextComponent != null)
//            {
//                StartCoroutine(MoveToHeight(scoreTextTransform, initialScoreTextY, moveDuration));
//                scoreTextComponent.gameObject.SetActive(false);
//            }
//        }
//    }


//    // 1. Sprite�̓����x���w��l�܂Ŋ��炩�ɕω�������R���[�`�� (�ύX�Ȃ�)
//    private IEnumerator FadeAlpha(SpriteRenderer sprite, float endAlpha, float duration)
//    {
//        if (sprite == null) yield break;

//        Color startColor = sprite.color;
//        float startAlpha = startColor.a;
//        float startTime = Time.time;

//        while (Time.time < startTime + duration)
//        {
//            float t = (Time.time - startTime) / duration;
//            Color newColor = sprite.color;
//            newColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
//            sprite.color = newColor;
//            yield return null;
//        }

//        Color finalColor = sprite.color;
//        finalColor.a = endAlpha;
//        sprite.color = finalColor;
//    }


//    // 2. �I�u�W�F�N�g���w�肵��Y���W�Ɋ��炩�Ɉړ�������R���[�`�� (�ύX�Ȃ�)
//    private IEnumerator MoveToHeight(Transform trans, float targetY, float duration)
//    {
//        if (trans == null) yield break;

//        Vector3 startPos = trans.position;
//        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);
//        float startTime = Time.time;

//        while (Time.time < startTime + duration)
//        {
//            float t = (Time.time - startTime) / duration;
//            trans.position = Vector3.Lerp(startPos, endPos, t);
//            yield return null;
//        }

//        trans.position = endPos;
//    }
//}

using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Text���g���ꍇ
using TMPro; // TextMeshProUGUI���g���ꍇ
using UnityEngine.SceneManagement; // �V�[���Ǘ��@�\���g�p���邽�߂ɕK�v

public class CheckboxEffectController_v4_Modified : MonoBehaviour
{
    // ===============================================
    // �� Inspector�Ŋm�F�E���삷�邽�߂̃u�[���l
    // ===============================================
    [Header("�� Inspector�p ����m�F�X�C�b�`")]
    [Tooltip("���̃`�F�b�N�{�b�N�X��ON/OFF�ŃG�t�F�N�g�𐧌䂵�܂��B")]
    public bool IsActivated = false;

    // �� �ǉ�: �e�X�g���[�h�p�`�F�b�N�{�b�N�X
    [Tooltip("�e�X�g�p�̃f�o�b�O���O���o�͂��A�G�t�F�N�g��ɃV�[�������[�h���邩�𐧌䂵�܂��B")]
    public bool IsTestMode = false;

    // �� �ǉ�: ���[�h����e�X�g�V�[����
    [Header("�� �e�X�g���[�h�ݒ�")]
    [Tooltip("IsTestMode��ON�̂Ƃ��AIsActivated��ON�ɂȂ�ƃ��[�h����V�[����")]
    public string TestSceneName = "TestScene";

    // ���݂̃u�[���l�̏�Ԃ�ێ����A�ω������o���邽�߂Ɏg�p
    private bool wasActivated = false;

    // ===============================================

    // === 1. �����x�ύX�iSprite Renderer���g�p�j�̐ݒ� ===
    [Header("1. �����x��ύX����摜 (SpriteRenderer)")]
    [Tooltip("��ł����삵�܂��B")]
    public SpriteRenderer spriteToFade;
    public float targetAlpha = 0.8f;      // �ύX��̖ڕW�����x (0.0 �` 1.0)
    public float fadeDuration = 0.5f;      // �t�F�[�h�ɂ����鎞��

    // === 2. �ʒu�ړ��iTransform���g�p�j�̐ݒ� ===
    [Header("2. �ړ��ݒ�̋��ʐݒ�")]
    public float moveDuration = 0.5f;      // �ړ��ɂ����鎞��

    [Header("2-1. �K�{�̈ړ��摜 (2��)")]
    [Tooltip("��ł����삵�܂��i���̏ꍇ�A�ړ������̓X�L�b�v����܂��j�B")]
    public Transform objectToMove2;
    public float targetHeightY2 = 100f;    // 2�ڂ̖ڕWY���W�i���[���h���W�j
    private float initialY2;               // 2�ڂ̉摜�̏���Y���W��ۑ�

    [Header("2-2. �I�v�V�����̈ړ��摜 (3��)")]
    [Tooltip("���̍��ڂ͋�ł����삵�܂��B")]
    public Transform objectToMove3;
    public float targetHeightY3 = 50f;     // 3�ڂ̖ڕWY���W�i���[���h���W�j
    private float initialY3;               // 3�ڂ̉摜�̏���Y���W��ۑ�

    // �� �C���E�ǉ�: �V�����ړ��摜 (4��) �̐ݒ�
    [Header("2-3. �ǉ��̈ړ��摜 (4��)")]
    [Tooltip("���̍��ڂ͋�ł����삵�܂��B")]
    public Transform objectToMove4;
    public float targetHeightY4 = -50f;     // 4�ڂ̖ڕWY���W�i���[���h���W�j
    private float initialY4;               // 4�ڂ̉摜�̏���Y���W��ۑ�


    // �� �C���E�ǉ�: �X�R�A�\���p�ݒ�
    [Header("3. �X�R�A�\���̐ݒ�")]
    [Tooltip("ScoreScript�ւ̎Q�ƁB��ł����삵�܂��B")]
    public ScoreScript scoreScript;          // ScoreScript�ւ̎Q�� (�X�R�A�l�̎擾�p)
    [Tooltip("�X�R�A�e�L�X�g��Transform�B��ł����삵�܂��B")]
    public Transform scoreTextTransform;     // �X�R�A�e�L�X�g��Transform (�ړ��E�\������p)
    private float initialScoreTextY;          // �X�R�A�e�L�X�g�̏���Y���W��ۑ�
    // �� �⑫: �X�R�A�e�L�X�g�̕\��/��\���Ɏg���R���|�[�l���g (Text��TextMeshProUGUI)
    private Component scoreTextComponent;

    // MonoBehaviour��Start���\�b�h�ŏ����ʒu���L�^
    void Start()
    {
        // 2�ڂ̈ړ��I�u�W�F�N�g�̏���Y���W��ۑ�
        if (objectToMove2 != null)
        {
            initialY2 = objectToMove2.position.y;
        }

        // 3�ڂ̈ړ��I�u�W�F�N�g�̏���Y���W��ۑ� (�I�v�V����)
        if (objectToMove3 != null)
        {
            initialY3 = objectToMove3.position.y;
        }

        // �� �ǉ�: 4�ڂ̈ړ��I�u�W�F�N�g�̏���Y���W��ۑ� (�I�v�V����)
        if (objectToMove4 != null)
        {
            initialY4 = objectToMove4.position.y;
        }

        // �X�R�A�e�L�X�g�̏���Y���W�ƃR���|�[�l���g��ۑ�
        if (scoreTextTransform != null)
        {
            initialScoreTextY = scoreTextTransform.position.y;

            // Text�܂���TextMeshProUGUI�R���|�[�l���g���擾���A�\��/��\���ɔ�����
            scoreTextComponent = scoreTextTransform.GetComponent<Text>();
            if (scoreTextComponent == null)
            {
                scoreTextComponent = scoreTextTransform.GetComponent<TextMeshProUGUI>();
            }

            // ������Ԃ��A�N�e�B�u�ɂ��� (�R���|�[�l���g�擾�������̂�)
            if (scoreTextComponent != null)
            {
                scoreTextComponent.gameObject.SetActive(false);
            }
        }

        // ������Ԃ��L�^
        wasActivated = IsActivated;
    }

    // ===============================================
    // Update�͕ύX�Ȃ�
    // ===============================================
    void Update()
    {
        // IsActivated �̒l���O�̃t���[������ω������ꍇ�̂ݏ��������s
        if (IsActivated != wasActivated)
        {
            // �A�j���[�V�������������s
            ExecuteEffects(IsActivated);

            // ��Ԃ��X�V
            wasActivated = IsActivated;
        }
    }
    // ===============================================

    // �S�ẴG�t�F�N�g�̊�����҂��A�Ō�ɃV�[�������[�h����R���[�`��
    private IEnumerator WaitForEffectsAndLoad(string sceneName)
    {
        // --- �G�t�F�N�g�̕�����s�J�n ---

        // 1. �����x�ύX�̊�����҂� (null�`�F�b�N�ς�)
        if (spriteToFade != null)
        {
            yield return StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));
        }

        // 2-1. objectToMove2�̈ړ��̊�����҂� (null�`�F�b�N�ς�)
        if (objectToMove2 != null)
        {
            yield return StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
        }
        else if (IsTestMode)
        {
            Debug.LogWarning("objectToMove2���ݒ肳��Ă��܂���B�ړ��������X�L�b�v���܂����B");
        }

        // 2-2. objectToMove3�̈ړ��̊�����҂� (null�`�F�b�N�ς�)
        if (objectToMove3 != null)
        {
            yield return StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
        }

        // 2-3. objectToMove4�̈ړ��̊�����҂� (���ǉ� null�`�F�b�N�ς�)
        if (objectToMove4 != null)
        {
            yield return StartCoroutine(MoveToHeight(objectToMove4, targetHeightY4, moveDuration));
        }

        // 3. �X�R�A�e�L�X�g�̏����ƈړ��̊�����҂� (null�`�F�b�N�ς�)
        // null�`�F�b�N�𑝂₵�A���f�����Ȃ��Ă����삷��悤�ɂ���
        if (scoreTextTransform != null && scoreTextComponent != null)
        {
            // �X�R�A�e�L�X�g�̕\����L���ɂ���
            scoreTextComponent.gameObject.SetActive(true);

            // �X�R�A�e�L�X�g�̈ړ�����
            yield return StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));

            // �X�R�A�l�̍X�V�i���f����null�łȂ��ꍇ�̂݁j
            if (scoreScript != null)
            {
                // �����ŃX�R�A�l�̍X�V�����i���̃X�N���v�g�ɂ̓X�R�A�l�̕\���������̂͂Ȃ������̂ŁA�����ł͈ړ��ƕ\���݂̂Ŋ����Ƃ��܂��j
                // ��: scoreScript.UpdateScoreDisplay(); 
            }
        }

        // --- �V�[�����[�h�O�̏��� ---
        Debug.Log("�e�X�g���[�h: �S�ẴG�t�F�N�g���������܂����B�V�[��: " + sceneName + "�����[�h���܂��B");

        // ���͂�����܂őҋ@
        while (!Input.anyKeyDown)
        {
            yield return null; // 1�t���[���ҋ@
        }

        // ���͂����o���ꂽ��V�[���J�ڂ����s
        SceneManager.LoadScene(sceneName);
    }


    // �e�X�g���[�hON�̏ꍇ�ɃV�[�P���X�R���[�`�����Ăяo��
    private void ExecuteEffects(bool isChecked)
    {
        if (isChecked)
        {
            // ===== ON (�`�F�b�N��) �̏��� =====

            if (IsTestMode)
            {
                // IsTestMode��ON�̏ꍇ: �G�t�F�N�g�����ԂɎ��s���A������ɃV�[�������[�h
                StartCoroutine(WaitForEffectsAndLoad(TestSceneName));
                return; // �V�[�P���X�R���[�`���ɏ������Ϗ����邽�߁A�����ŏI��
            }

            // --- IsTestMode��OFF�̏ꍇ�i�ʏ�̓���: �G�t�F�N�g����s���Ď��s�j ---

            // 1. �����x�ύX
            StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));

            // 2. �ړ�����
            // null�`�F�b�N��O��
            if (objectToMove2 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
            }
            if (objectToMove3 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
            }
            // �� �ǉ�: 4�ڂ̈ړ��摜
            if (objectToMove4 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove4, targetHeightY4, moveDuration));
            }

            // 3. �X�R�A�e�L�X�g�̏���
            if (scoreTextTransform != null && scoreTextComponent != null)
            {
                scoreTextComponent.gameObject.SetActive(true);
                StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));
                // ���f����null�łȂ��ꍇ�̂݃X�R�A�l�̍X�V���s��
                if (scoreScript != null)
                {
                    // ��: scoreScript.UpdateScoreDisplay();
                }
            }
        }
        else
        {
            // ===== OFF (�`�F�b�N������) �̏��� =====
            if (IsTestMode)
            {
                Debug.Log("IsActivated��OFF�ɂȂ�܂����B(�e�X�g���[�h)");
            }

            // 1. �����x�ύX (null�`�F�b�N��ǉ�)
            StartCoroutine(FadeAlpha(spriteToFade, 1.0f, fadeDuration));

            // 2. �ړ����� (null�`�F�b�N��O��)
            if (objectToMove2 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove2, initialY2, moveDuration));
            }
            if (objectToMove3 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove3, initialY3, moveDuration));
            }
            // �� �ǉ�: 4�ڂ̈ړ��摜
            if (objectToMove4 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove4, initialY4, moveDuration));
            }

            // 3. �X�R�A�e�L�X�g�̏��� (null�`�F�b�N��O��)
            if (scoreTextTransform != null && scoreTextComponent != null)
            {
                StartCoroutine(MoveToHeight(scoreTextTransform, initialScoreTextY, moveDuration));
                // �ړ�������҂����ɔ�\���ɂ���ꍇ�͂�����B
                // ��茵���ɂ́AMoveToHeight�R���[�`���̌��SetActive(false)�����s���ׂ��ł����A
                // �����OFF�����͕��s���s�Ȃ̂ŁA����ŗǂ��Ƃ��Ă��܂��B
                scoreTextComponent.gameObject.SetActive(false);
            }
        }
    }


    // 1. Sprite�̓����x���w��l�܂Ŋ��炩�ɕω�������R���[�`�� (�ύX�Ȃ�)
    private IEnumerator FadeAlpha(SpriteRenderer sprite, float endAlpha, float duration)
    {
        if (sprite == null) yield break;

        Color startColor = sprite.color;
        float startAlpha = startColor.a;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color newColor = sprite.color;
            newColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            sprite.color = newColor;
            yield return null;
        }

        Color finalColor = sprite.color;
        finalColor.a = endAlpha;
        sprite.color = finalColor;
    }


    // 2. �I�u�W�F�N�g���w�肵��Y���W�Ɋ��炩�Ɉړ�������R���[�`�� (�ύX�Ȃ�)
    private IEnumerator MoveToHeight(Transform trans, float targetY, float duration)
    {
        // �� �C���_: null�`�F�b�N��ǉ�
        if (trans == null) yield break;

        Vector3 startPos = trans.position;
        Vector3 endPos = new Vector3(startPos.x, targetY, startPos.z);
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            trans.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        trans.position = endPos;
    }
}