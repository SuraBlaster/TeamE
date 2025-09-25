//using UnityEngine;
//using System.Collections;

//public class ChangeColorOnce : MonoBehaviour
//{
//    // インスペクターで設定するチェックボックス
//    public bool changeColor = false;

//    // モデルのRendererコンポーネント
//    private Renderer modelRenderer;

//    // 元の色
//    private Color originalColor;

//    // 一時的に変わる色
//    public Color newColor = Color.white;

//    // 色が変わっている時間を指定
//    public float changeDuration = 0.1f;

//    // 処理が一度だけ実行されるようにするためのフラグ
//    private bool hasChanged = false;

//    void Start()
//    {
//        // モデルのRendererコンポーネントを取得
//        modelRenderer = GetComponentInChildren<Renderer>();
//        if (modelRenderer != null)
//        {
//            // 元の色を保存
//            originalColor = modelRenderer.material.color;
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
//            modelRenderer.material.color = newColor;

//            // 指定した時間だけ待機
//            yield return new WaitForSeconds(changeDuration);

//            // 元の色に戻す
//            modelRenderer.material.color = originalColor;
//        }

//        // changeColorをfalseに戻し、hasChangedをリセット
//        changeColor = false;
//        hasChanged = false;
//    }
//}


using UnityEngine;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    // インスペクターで設定するチェックボックス
    public bool changeColor = false;

    // モデルのRendererコンポーネント
    private SpriteRenderer modelRenderer; // RendererからSpriteRendererに変更

    // 元の色
    private Color originalColor;

    // 一時的に変わる色
    public Color newColor = Color.red;

    // 色が変わっている時間を指定
    public float changeDuration = 0.1f;

    // 処理が一度だけ実行されるようにするためのフラグ
    private bool hasChanged = false;

    void Start()
    {
        // モデルのRendererコンポーネントを取得
        modelRenderer = GetComponentInChildren<SpriteRenderer>(); // GetComponentInChildren<SpriteRenderer>()に変更
        if (modelRenderer != null)
        {
            // 元の色を保存
            originalColor = modelRenderer.color; // material.colorから.colorに変更
        }
    }

    void Update()
    {
        // changeColorがtrueで、かつまだ処理を実行していない場合
        if (changeColor && !hasChanged)
        {
            // 色の変更処理を開始
            StartCoroutine(ColorChangeEffect());
        }
    }

    // 色の変更処理をコルーチンで行う
    private IEnumerator ColorChangeEffect()
    {
        // 処理が始まったことを記録
        hasChanged = true;

        if (modelRenderer != null)
        {
            // 色を一時的に変更
            modelRenderer.color = newColor; // material.colorから.colorに変更

            // 指定した時間だけ待機
            yield return new WaitForSeconds(changeDuration);

            // 元の色に戻す
            modelRenderer.color = originalColor; // material.colorから.colorに変更
        }

        // changeColorをfalseに戻し、hasChangedをリセット
        changeColor = false;
        hasChanged = false;
    }
}