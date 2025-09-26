//using UnityEngine;
//using System.Collections;

//public class DamageEffect : MonoBehaviour
//{
//    // インスペクターで設定するチェックボックス
//    public bool changeColor = false;

//    // モデルのRendererコンポーネント
//    private SpriteRenderer modelRenderer; // RendererからSpriteRendererに変更

//    // 元の色
//    private Color originalColor;

//    // 一時的に変わる色
//    public Color newColor = Color.red;

//    // 色が変わっている時間を指定
//    public float changeDuration = 0.1f;

//    // 処理が一度だけ実行されるようにするためのフラグ
//    private bool hasChanged = false;

//    void Start()
//    {
//        // モデルのRendererコンポーネントを取得
//        modelRenderer = GetComponentInChildren<SpriteRenderer>(); // GetComponentInChildren<SpriteRenderer>()に変更
//        if (modelRenderer != null)
//        {
//            // 元の色を保存
//            originalColor = modelRenderer.color; // material.colorから.colorに変更
//        }
//    }

//    void Update()
//    {
//        // changeColorがtrueで、かつまだ処理を実行していない場合
//        if (changeColor && !hasChanged)
//        {
//            // 色の変更処理を開始
//            StartCoroutine(ColorChangeEffect());
//        }
//    }

//    // 色の変更処理をコルーチンで行う
//    private IEnumerator ColorChangeEffect()
//    {
//        // 処理が始まったことを記録
//        hasChanged = true;

//        if (modelRenderer != null)
//        {
//            // 色を一時的に変更
//            modelRenderer.color = newColor; // material.colorから.colorに変更

//            // 指定した時間だけ待機
//            yield return new WaitForSeconds(changeDuration);

//            // 元の色に戻す
//            modelRenderer.color = originalColor; // material.colorから.colorに変更
//        }

//        // changeColorをfalseに戻し、hasChangedをリセット
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

    [Header("フラッシュ設定")]
    public Color flashColor = Color.white;  // フラッシュ時の色
    [Range(0f, 1f)]
    public float flashAlpha = 0.3f;         // フラッシュ時の透明度（0=完全透明, 1=不透明）
    public float flashDuration = 0.3f;      // フラッシュ時間
    public int flashCount = 1;              // 点滅回数

    [Header("テスト用")]
    public bool isDamaged = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 元の色を保存
    }

    void Update()
    {
        // テスト用: インスペクタでチェックされたら実行
        if (isDamaged)
        {
            isDamaged = false; // 一回でリセット
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // フラッシュカラー + 指定した透明度に変更
            spriteRenderer.color = new Color(
                flashColor.r,
                flashColor.g,
                flashColor.b,
                flashAlpha
            );
            yield return new WaitForSeconds(flashDuration / 2f);

            // 元の色に戻す
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration / 2f);
        }
    }

    // 実際のダメージ処理から呼び出す用
    public void OnDamage()
    {
        StartCoroutine(FlashRoutine());
    }
}
