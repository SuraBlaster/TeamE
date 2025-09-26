//using UnityEngine;
//using System.Collections;

//public class DamageEffect : MonoBehaviour
//{
//    // �C���X�y�N�^�[�Őݒ肷��`�F�b�N�{�b�N�X
//    public bool changeColor = false;

//    // ���f����Renderer�R���|�[�l���g
//    private SpriteRenderer modelRenderer; // Renderer����SpriteRenderer�ɕύX

//    // ���̐F
//    private Color originalColor;

//    // �ꎞ�I�ɕς��F
//    public Color newColor = Color.red;

//    // �F���ς���Ă��鎞�Ԃ��w��
//    public float changeDuration = 0.1f;

//    // ��������x�������s�����悤�ɂ��邽�߂̃t���O
//    private bool hasChanged = false;

//    void Start()
//    {
//        // ���f����Renderer�R���|�[�l���g���擾
//        modelRenderer = GetComponentInChildren<SpriteRenderer>(); // GetComponentInChildren<SpriteRenderer>()�ɕύX
//        if (modelRenderer != null)
//        {
//            // ���̐F��ۑ�
//            originalColor = modelRenderer.color; // material.color����.color�ɕύX
//        }
//    }

//    void Update()
//    {
//        // changeColor��true�ŁA���܂����������s���Ă��Ȃ��ꍇ
//        if (changeColor && !hasChanged)
//        {
//            // �F�̕ύX�������J�n
//            StartCoroutine(ColorChangeEffect());
//        }
//    }

//    // �F�̕ύX�������R���[�`���ōs��
//    private IEnumerator ColorChangeEffect()
//    {
//        // �������n�܂������Ƃ��L�^
//        hasChanged = true;

//        if (modelRenderer != null)
//        {
//            // �F���ꎞ�I�ɕύX
//            modelRenderer.color = newColor; // material.color����.color�ɕύX

//            // �w�肵�����Ԃ����ҋ@
//            yield return new WaitForSeconds(changeDuration);

//            // ���̐F�ɖ߂�
//            modelRenderer.color = originalColor; // material.color����.color�ɕύX
//        }

//        // changeColor��false�ɖ߂��AhasChanged�����Z�b�g
//        changeColor = false;
//        hasChanged = false;
//    }
//}

using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("�t���b�V���ݒ�")]
    public Color flashColor = Color.white;  // �t���b�V�����̐F
    [Range(0f, 1f)]
    public float flashAlpha = 0.3f;         // �t���b�V�����̓����x�i0=���S����, 1=�s�����j
    public float flashDuration = 0.3f;      // �t���b�V������
    public int flashCount = 1;              // �_�ŉ�

    [Header("�e�X�g�p")]
    public bool isDamaged = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // ���̐F��ۑ�
    }

    void Update()
    {
        // �e�X�g�p: �C���X�y�N�^�Ń`�F�b�N���ꂽ����s
        if (isDamaged)
        {
            isDamaged = false; // ���Ń��Z�b�g
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // �t���b�V���J���[ + �w�肵�������x�ɕύX
            spriteRenderer.color = new Color(
                flashColor.r,
                flashColor.g,
                flashColor.b,
                flashAlpha
            );
            yield return new WaitForSeconds(flashDuration / 2f);

            // ���̐F�ɖ߂�
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration / 2f);
        }
    }

    // ���ۂ̃_���[�W��������Ăяo���p
    public void OnDamage()
    {
        StartCoroutine(FlashRoutine());
    }
}
