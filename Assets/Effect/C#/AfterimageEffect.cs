using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public GameObject afterImagePrefab; // 残像Prefab
    public float interval = 0.05f;      // 残像を出す間隔

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

        // 現在のフレームのスプライトをコピー
        sr.sprite = mainSr.sprite;

        // flip情報コピー
        sr.flipX = mainSr.flipX;
        sr.flipY = mainSr.flipY;

        // カラーもコピー（点滅やカラー変更がある場合）
        sr.color = mainSr.color;

        // ソート順（本体より少し奥）
        sr.sortingLayerID = mainSr.sortingLayerID;
        sr.sortingOrder = mainSr.sortingOrder - 1;
    }
}
