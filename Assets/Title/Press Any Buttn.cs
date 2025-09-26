using UnityEngine;
using UnityEngine.UI; // UI���g�����߂ɕK�v

public class BlinkingText : MonoBehaviour
{
    public float blinkSpeed = 2f;    // �_�ł̑���
    public float minAlpha = 0.2f;    // �ŏ��̓����x (0.0f�Ŋ��S�ɓ���, 1.0f�Ŋ��S�ɕs����)
    public float maxAlpha = 1.0f;    // �ő�̓����x

    private Text textComponent;

    void Start()
    {
        // Text�R���|�[�l���g���擾
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.LogError("Text component not found!");
            enabled = false; // �R���|�[�l���g���Ȃ���΃X�N���v�g�𖳌���
            return;
        }
    }

    void Update()
    {
        // ���Ԃ�Sin�֐����g����-1.0����1.0�̊Ԃ����炩�ɕω�����l���擾
        float rawAlpha = Mathf.Sin(Time.time * blinkSpeed);

        // rawAlpha��0.0����1.0�͈̔͂ɐ��K��
        // (Sin�֐���-1����1�Ȃ̂ŁA(�l + 1) / 2 ��0����1�͈̔͂ɕϊ�)
        float normalizedAlpha = (rawAlpha + 1f) / 2f;

        // �ŏ��A���t�@�ƍő�A���t�@�̊ԂŐ��`���
        float finalAlpha = Mathf.Lerp(minAlpha, maxAlpha, normalizedAlpha);

        // ���݂̐F���擾
        Color newColor = textComponent.color;

        // A�i�A���t�@�j�l�݂̂�ݒ� (0.0f����1.0f�͈̔͂�)
        newColor.a = finalAlpha;

        // �e�L�X�g�ɐV�����F��ݒ�
        textComponent.color = newColor;
    }
}