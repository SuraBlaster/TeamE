using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public GameObject afterImagePrefab; // �c��Prefab
    public float interval = 0.05f;      // �c�����o���Ԋu

    private float timer;
    private SpriteRenderer mainSr;

    void Start()
    {
        mainSr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }

    void SpawnAfterImage()
    {
        GameObject obj = Instantiate(afterImagePrefab, transform.position, transform.rotation);

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        // ���݂̃t���[���̃X�v���C�g���R�s�[
        sr.sprite = mainSr.sprite;

        // flip���R�s�[
        sr.flipX = mainSr.flipX;
        sr.flipY = mainSr.flipY;

        // �J���[���R�s�[�i�_�ł�J���[�ύX������ꍇ�j
        sr.color = mainSr.color;

        // �\�[�g���i�{�̂�菭�����j
        sr.sortingLayerID = mainSr.sortingLayerID;
        sr.sortingOrder = mainSr.sortingOrder - 1;
    }
}
