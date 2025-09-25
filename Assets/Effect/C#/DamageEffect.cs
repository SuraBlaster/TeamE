//using UnityEngine;
//using System.Collections;

//public class ChangeColorOnce : MonoBehaviour
//{
//    // �C���X�y�N�^�[�Őݒ肷��`�F�b�N�{�b�N�X
//    public bool changeColor = false;

//    // ���f����Renderer�R���|�[�l���g
//    private Renderer modelRenderer;

//    // ���̐F
//    private Color originalColor;

//    // �ꎞ�I�ɕς��F
//    public Color newColor = Color.white;

//    // �F���ς���Ă��鎞�Ԃ��w��
//    public float changeDuration = 0.1f;

//    // ��������x�������s�����悤�ɂ��邽�߂̃t���O
//    private bool hasChanged = false;

//    void Start()
//    {
//        // ���f����Renderer�R���|�[�l���g���擾
//        modelRenderer = GetComponentInChildren<Renderer>();
//        if (modelRenderer != null)
//        {
//            // ���̐F��ۑ�
//            originalColor = modelRenderer.material.color;
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
//            modelRenderer.material.color = newColor;

//            // �w�肵�����Ԃ����ҋ@
//            yield return new WaitForSeconds(changeDuration);

//            // ���̐F�ɖ߂�
//            modelRenderer.material.color = originalColor;
//        }

//        // changeColor��false�ɖ߂��AhasChanged�����Z�b�g
//        changeColor = false;
//        hasChanged = false;
//    }
//}


using UnityEngine;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    // �C���X�y�N�^�[�Őݒ肷��`�F�b�N�{�b�N�X
    public bool changeColor = false;

    // ���f����Renderer�R���|�[�l���g
    private SpriteRenderer modelRenderer; // Renderer����SpriteRenderer�ɕύX

    // ���̐F
    private Color originalColor;

    // �ꎞ�I�ɕς��F
    public Color newColor = Color.red;

    // �F���ς���Ă��鎞�Ԃ��w��
    public float changeDuration = 0.1f;

    // ��������x�������s�����悤�ɂ��邽�߂̃t���O
    private bool hasChanged = false;

    void Start()
    {
        // ���f����Renderer�R���|�[�l���g���擾
        modelRenderer = GetComponentInChildren<SpriteRenderer>(); // GetComponentInChildren<SpriteRenderer>()�ɕύX
        if (modelRenderer != null)
        {
            // ���̐F��ۑ�
            originalColor = modelRenderer.color; // material.color����.color�ɕύX
        }
    }

    void Update()
    {
        // changeColor��true�ŁA���܂����������s���Ă��Ȃ��ꍇ
        if (changeColor && !hasChanged)
        {
            // �F�̕ύX�������J�n
            StartCoroutine(ColorChangeEffect());
        }
    }

    // �F�̕ύX�������R���[�`���ōs��
    private IEnumerator ColorChangeEffect()
    {
        // �������n�܂������Ƃ��L�^
        hasChanged = true;

        if (modelRenderer != null)
        {
            // �F���ꎞ�I�ɕύX
            modelRenderer.color = newColor; // material.color����.color�ɕύX

            // �w�肵�����Ԃ����ҋ@
            yield return new WaitForSeconds(changeDuration);

            // ���̐F�ɖ߂�
            modelRenderer.color = originalColor; // material.color����.color�ɕύX
        }

        // changeColor��false�ɖ߂��AhasChanged�����Z�b�g
        changeColor = false;
        hasChanged = false;
    }
}