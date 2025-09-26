//using UnityEngine;

//public class Afterimage : MonoBehaviour
//{
//    public float lifeTime = 0.5f; // �c����������܂ł̎���
//    private SpriteRenderer sr;
//    private Color color;

//    void Start()
//    {
//        sr = GetComponent<SpriteRenderer>();
//        color = sr.color;
//        Destroy(gameObject, lifeTime); // ��莞�Ԍ�Ɏ����폜
//    }

//    void Update()
//    {
//        color.a -= Time.deltaTime / lifeTime; // �A���t�@�l�����X�Ɍ���
//        sr.color = color;
//    }
//}

using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float lifeTime = 0.2f; // �c���̐�������
    private SpriteRenderer sr;
    private Color color;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    void Update()
    {
        if (sr == null) return;

        // �o�ߎ��Ԃ����Z
        timer += Time.deltaTime;

        // �A���t�@�l�� 1 �� 0 �ɕ��
        color.a = Mathf.Lerp(1f, 0f, timer / lifeTime);
        sr.color = color;

        // �����𒴂�����폜
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}

