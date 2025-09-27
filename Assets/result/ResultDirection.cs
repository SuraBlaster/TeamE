//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI; // Textを使う場合
//using TMPro; // TextMeshProUGUIを使う場合
//using UnityEngine.SceneManagement; // ★ 追加: シーン管理機能を使用するために必要

//public class CheckboxEffectController_v4 : MonoBehaviour
//{
//    // ===============================================
//    // ★ 修正点: Inspectorで確認・操作するためのブール値
//    // ===============================================
//    [Header("★ Inspector用 動作確認スイッチ")]
//    [Tooltip("このチェックボックスのON/OFFでエフェクトを制御します。")]
//    public bool IsActivated = false;

//    // ★ 追加: テストモード用チェックボックス
//    [Tooltip("テスト用のデバッグログを出力し、エフェクト後にシーンをロードするかを制御します。")]
//    public bool IsTestMode = false;

//    // ★ 追加: ロードするテストシーン名
//    [Header("★ テストモード設定")]
//    [Tooltip("IsTestModeがONのとき、IsActivatedがONになるとロードするシーン名")]
//    public string TestSceneName = "TestScene";

//    // 現在のブール値の状態を保持し、変化を検出するために使用
//    private bool wasActivated = false;

//    // ===============================================

//    // === 1. 透明度変更（Sprite Rendererを使用）の設定 ===
//    [Header("1. 透明度を変更する画像 (SpriteRenderer)")]
//    public SpriteRenderer spriteToFade;
//    public float targetAlpha = 0.8f;      // 変更後の目標透明度 (0.0 ～ 1.0)
//    public float fadeDuration = 0.5f;      // フェードにかける時間

//    // === 2. 位置移動（Transformを使用）の設定 ===
//    [Header("2. 移動設定の共通設定")]
//    public float moveDuration = 0.5f;      // 移動にかける時間

//    [Header("2-1. 必須の移動画像 (2つ目)")]
//    public Transform objectToMove2;
//    public float targetHeightY2 = 100f;    // 2つ目の目標Y座標（ワールド座標）
//    private float initialY2;               // 2つ目の画像の初期Y座標を保存

//    [Header("2-2. オプションの移動画像 (3つ目)")]
//    [Tooltip("この項目は空でも動作します。")]
//    public Transform objectToMove3;
//    public float targetHeightY3 = 50f;     // 3つ目の目標Y座標（ワールド座標）
//    private float initialY3;               // 3つ目の画像の初期Y座標を保存


//    // ★ 修正・追加: スコア表示用設定
//    [Header("3. スコア表示の設定")]
//    public ScoreScript scoreScript;          // ScoreScriptへの参照 (スコア値の取得用)
//    [Tooltip("スコアテキストのTransformを割り当ててください。")]
//    public Transform scoreTextTransform;     // スコアテキストのTransform (移動・表示制御用)
//    private float initialScoreTextY;          // スコアテキストの初期Y座標を保存
//    // ★ 補足: スコアテキストの表示/非表示に使うコンポーネント (TextかTextMeshProUGUI)
//    private Component scoreTextComponent;

//    // MonoBehaviourのStartメソッドで初期位置を記録
//    void Start()
//    {
//        // 2つ目の移動オブジェクトの初期Y座標を保存 (必須)
//        if (objectToMove2 != null)
//        {
//            initialY2 = objectToMove2.position.y;
//        }

//        // 3つ目の移動オブジェクトの初期Y座標を保存 (オプション)
//        if (objectToMove3 != null)
//        {
//            initialY3 = objectToMove3.position.y;
//        }

//        // ★ 修正: スコアテキストの初期Y座標とコンポーネントを保存
//        if (scoreTextTransform != null)
//        {
//            initialScoreTextY = scoreTextTransform.position.y;

//            // TextまたはTextMeshProUGUIコンポーネントを取得し、表示/非表示に備える
//            scoreTextComponent = scoreTextTransform.GetComponent<Text>();
//            if (scoreTextComponent == null)
//            {
//                scoreTextComponent = scoreTextTransform.GetComponent<TextMeshProUGUI>();
//            }

//            // 初期状態を非アクティブにする
//            if (scoreTextComponent != null)
//            {
//                scoreTextComponent.gameObject.SetActive(false);
//            }
//        }

//        // 初期状態を記録
//        wasActivated = IsActivated;
//    }

//    // ===============================================
//    // Updateは変更なし
//    // ===============================================
//    void Update()
//    {
//        // IsActivated の値が前のフレームから変化した場合のみ処理を実行
//        if (IsActivated != wasActivated)
//        {
//            // アニメーション処理を実行
//            ExecuteEffects(IsActivated);

//            // 状態を更新
//            wasActivated = IsActivated;
//        }
//    }
//    // ===============================================

//    // ★ 追加: 全てのエフェクトの完了を待ち、最後にシーンをロードするコルーチン
//    private IEnumerator WaitForEffectsAndLoad(string sceneName)
//    {
//        // 1. 透明度変更の完了を待つ
//        if (spriteToFade != null)
//        {
//            yield return StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));
//        }

//        // 2. objectToMove2の移動の完了を待つ
//        if (objectToMove2 == null)
//        {
//            Debug.LogError("objectToMove2が設定されていません。移動処理をスキップします。");
//        }
//        else
//        {
//            yield return StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
//        }

//        // 3. objectToMove3の移動の完了を待つ
//        if (objectToMove3 != null)
//        {
//            yield return StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
//        }

//        // 4. スコアテキストの処理と移動の完了を待つ
//        if (scoreTextTransform != null && scoreTextComponent != null)
//        {
//            scoreTextComponent.gameObject.SetActive(true);
//            yield return StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));
//        }

//        // 全てのエフェクトが完了したらシーンをロード
//        Debug.Log("テストモード: 全てのエフェクトが完了しました。シーン: " + sceneName + "をロードします。");
//        // **while ループと yield return null** を使って、入力があるまで待機
//        while (!Input.anyKeyDown)
//        {
//            yield return null; // 1フレーム待機
//        }
//        // 入力が検出されたら、コルーチンを開始してフェードアウトとシーン遷移を実行
//        SceneManager.LoadScene(sceneName);

//    }


//    // ★ 修正: テストモードONの場合にシーケンスコルーチンを呼び出す
//    private void ExecuteEffects(bool isChecked)
//    {
//        if (isChecked)
//        {
//            // ===== ON (チェック時) の処理 =====

//            if (IsTestMode)
//            {
//                // IsTestModeがONの場合: エフェクトを順番に実行し、完了後にシーンをロード
//                StartCoroutine(WaitForEffectsAndLoad(TestSceneName));
//                return; // シーケンスコルーチンに処理を委譲するため、ここで終了
//            }

//            // --- IsTestModeがOFFの場合（通常の動作: エフェクトを並行して実行） ---
//            StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));

//            if (objectToMove2 == null)
//            {
//                Debug.LogError("objectToMove2 (2つ目の画像) が設定されていません。");
//            }
//            else
//            {
//                StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
//            }

//            if (objectToMove3 != null)
//            {
//                StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
//            }

//            // スコアテキストの処理
//            if (scoreTextTransform != null && scoreTextComponent != null)
//            {
//                scoreTextComponent.gameObject.SetActive(true);
//                StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));
//            }
//        }
//        else
//        {
//            // ===== OFF (チェック解除時) の処理 (シーンロードがないため変更なし) =====
//            if (IsTestMode)
//            {
//                Debug.Log("IsActivatedがOFFになりました。(テストモード)");
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

//            // スコアテキストの処理
//            if (scoreTextTransform != null && scoreTextComponent != null)
//            {
//                StartCoroutine(MoveToHeight(scoreTextTransform, initialScoreTextY, moveDuration));
//                scoreTextComponent.gameObject.SetActive(false);
//            }
//        }
//    }


//    // 1. Spriteの透明度を指定値まで滑らかに変化させるコルーチン (変更なし)
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


//    // 2. オブジェクトを指定したY座標に滑らかに移動させるコルーチン (変更なし)
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
using UnityEngine.UI; // Textを使う場合
using TMPro; // TextMeshProUGUIを使う場合
using UnityEngine.SceneManagement; // シーン管理機能を使用するために必要

public class CheckboxEffectController_v4_Modified : MonoBehaviour
{
    // ===============================================
    // ★ Inspectorで確認・操作するためのブール値
    // ===============================================
    [Header("★ Inspector用 動作確認スイッチ")]
    [Tooltip("このチェックボックスのON/OFFでエフェクトを制御します。")]
    public bool IsActivated = false;

    // ★ 追加: テストモード用チェックボックス
    [Tooltip("テスト用のデバッグログを出力し、エフェクト後にシーンをロードするかを制御します。")]
    public bool IsTestMode = false;

    // ★ 追加: ロードするテストシーン名
    [Header("★ テストモード設定")]
    [Tooltip("IsTestModeがONのとき、IsActivatedがONになるとロードするシーン名")]
    public string TestSceneName = "TestScene";

    // 現在のブール値の状態を保持し、変化を検出するために使用
    private bool wasActivated = false;

    // ===============================================

    // === 1. 透明度変更（Sprite Rendererを使用）の設定 ===
    [Header("1. 透明度を変更する画像 (SpriteRenderer)")]
    [Tooltip("空でも動作します。")]
    public SpriteRenderer spriteToFade;
    public float targetAlpha = 0.8f;      // 変更後の目標透明度 (0.0 ～ 1.0)
    public float fadeDuration = 0.5f;      // フェードにかける時間

    // === 2. 位置移動（Transformを使用）の設定 ===
    [Header("2. 移動設定の共通設定")]
    public float moveDuration = 0.5f;      // 移動にかける時間

    [Header("2-1. 必須の移動画像 (2つ目)")]
    [Tooltip("空でも動作します（その場合、移動処理はスキップされます）。")]
    public Transform objectToMove2;
    public float targetHeightY2 = 100f;    // 2つ目の目標Y座標（ワールド座標）
    private float initialY2;               // 2つ目の画像の初期Y座標を保存

    [Header("2-2. オプションの移動画像 (3つ目)")]
    [Tooltip("この項目は空でも動作します。")]
    public Transform objectToMove3;
    public float targetHeightY3 = 50f;     // 3つ目の目標Y座標（ワールド座標）
    private float initialY3;               // 3つ目の画像の初期Y座標を保存

    // ★ 修正・追加: 新しい移動画像 (4つ目) の設定
    [Header("2-3. 追加の移動画像 (4つ目)")]
    [Tooltip("この項目は空でも動作します。")]
    public Transform objectToMove4;
    public float targetHeightY4 = -50f;     // 4つ目の目標Y座標（ワールド座標）
    private float initialY4;               // 4つ目の画像の初期Y座標を保存


    // ★ 修正・追加: スコア表示用設定
    [Header("3. スコア表示の設定")]
    [Tooltip("ScoreScriptへの参照。空でも動作します。")]
    public ScoreScript scoreScript;          // ScoreScriptへの参照 (スコア値の取得用)
    [Tooltip("スコアテキストのTransform。空でも動作します。")]
    public Transform scoreTextTransform;     // スコアテキストのTransform (移動・表示制御用)
    private float initialScoreTextY;          // スコアテキストの初期Y座標を保存
    // ★ 補足: スコアテキストの表示/非表示に使うコンポーネント (TextかTextMeshProUGUI)
    private Component scoreTextComponent;

    // MonoBehaviourのStartメソッドで初期位置を記録
    void Start()
    {
        // 2つ目の移動オブジェクトの初期Y座標を保存
        if (objectToMove2 != null)
        {
            initialY2 = objectToMove2.position.y;
        }

        // 3つ目の移動オブジェクトの初期Y座標を保存 (オプション)
        if (objectToMove3 != null)
        {
            initialY3 = objectToMove3.position.y;
        }

        // ★ 追加: 4つ目の移動オブジェクトの初期Y座標を保存 (オプション)
        if (objectToMove4 != null)
        {
            initialY4 = objectToMove4.position.y;
        }

        // スコアテキストの初期Y座標とコンポーネントを保存
        if (scoreTextTransform != null)
        {
            initialScoreTextY = scoreTextTransform.position.y;

            // TextまたはTextMeshProUGUIコンポーネントを取得し、表示/非表示に備える
            scoreTextComponent = scoreTextTransform.GetComponent<Text>();
            if (scoreTextComponent == null)
            {
                scoreTextComponent = scoreTextTransform.GetComponent<TextMeshProUGUI>();
            }

            // 初期状態を非アクティブにする (コンポーネント取得成功時のみ)
            if (scoreTextComponent != null)
            {
                scoreTextComponent.gameObject.SetActive(false);
            }
        }

        // 初期状態を記録
        wasActivated = IsActivated;
    }

    // ===============================================
    // Updateは変更なし
    // ===============================================
    void Update()
    {
        // IsActivated の値が前のフレームから変化した場合のみ処理を実行
        if (IsActivated != wasActivated)
        {
            // アニメーション処理を実行
            ExecuteEffects(IsActivated);

            // 状態を更新
            wasActivated = IsActivated;
        }
    }
    // ===============================================

    // 全てのエフェクトの完了を待ち、最後にシーンをロードするコルーチン
    private IEnumerator WaitForEffectsAndLoad(string sceneName)
    {
        // --- エフェクトの並列実行開始 ---

        // 1. 透明度変更の完了を待つ (nullチェック済み)
        if (spriteToFade != null)
        {
            yield return StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));
        }

        // 2-1. objectToMove2の移動の完了を待つ (nullチェック済み)
        if (objectToMove2 != null)
        {
            yield return StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
        }
        else if (IsTestMode)
        {
            Debug.LogWarning("objectToMove2が設定されていません。移動処理をスキップしました。");
        }

        // 2-2. objectToMove3の移動の完了を待つ (nullチェック済み)
        if (objectToMove3 != null)
        {
            yield return StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
        }

        // 2-3. objectToMove4の移動の完了を待つ (★追加 nullチェック済み)
        if (objectToMove4 != null)
        {
            yield return StartCoroutine(MoveToHeight(objectToMove4, targetHeightY4, moveDuration));
        }

        // 3. スコアテキストの処理と移動の完了を待つ (nullチェック済み)
        // nullチェックを増やし、モデルがなくても動作するようにする
        if (scoreTextTransform != null && scoreTextComponent != null)
        {
            // スコアテキストの表示を有効にする
            scoreTextComponent.gameObject.SetActive(true);

            // スコアテキストの移動処理
            yield return StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));

            // スコア値の更新（モデルがnullでない場合のみ）
            if (scoreScript != null)
            {
                // ここでスコア値の更新処理（元のスクリプトにはスコア値の表示処理自体はなかったので、ここでは移動と表示のみで完了とします）
                // 例: scoreScript.UpdateScoreDisplay(); 
            }
        }

        // --- シーンロード前の処理 ---
        Debug.Log("テストモード: 全てのエフェクトが完了しました。シーン: " + sceneName + "をロードします。");

        // 入力があるまで待機
        while (!Input.anyKeyDown)
        {
            yield return null; // 1フレーム待機
        }

        // 入力が検出されたらシーン遷移を実行
        SceneManager.LoadScene(sceneName);
    }


    // テストモードONの場合にシーケンスコルーチンを呼び出す
    private void ExecuteEffects(bool isChecked)
    {
        if (isChecked)
        {
            // ===== ON (チェック時) の処理 =====

            if (IsTestMode)
            {
                // IsTestModeがONの場合: エフェクトを順番に実行し、完了後にシーンをロード
                StartCoroutine(WaitForEffectsAndLoad(TestSceneName));
                return; // シーケンスコルーチンに処理を委譲するため、ここで終了
            }

            // --- IsTestModeがOFFの場合（通常の動作: エフェクトを並行して実行） ---

            // 1. 透明度変更
            StartCoroutine(FadeAlpha(spriteToFade, targetAlpha, fadeDuration));

            // 2. 移動処理
            // nullチェックを徹底
            if (objectToMove2 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove2, targetHeightY2, moveDuration));
            }
            if (objectToMove3 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove3, targetHeightY3, moveDuration));
            }
            // ★ 追加: 4つ目の移動画像
            if (objectToMove4 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove4, targetHeightY4, moveDuration));
            }

            // 3. スコアテキストの処理
            if (scoreTextTransform != null && scoreTextComponent != null)
            {
                scoreTextComponent.gameObject.SetActive(true);
                StartCoroutine(MoveToHeight(scoreTextTransform, targetHeightY3, moveDuration));
                // モデルがnullでない場合のみスコア値の更新を行う
                if (scoreScript != null)
                {
                    // 例: scoreScript.UpdateScoreDisplay();
                }
            }
        }
        else
        {
            // ===== OFF (チェック解除時) の処理 =====
            if (IsTestMode)
            {
                Debug.Log("IsActivatedがOFFになりました。(テストモード)");
            }

            // 1. 透明度変更 (nullチェックを追加)
            StartCoroutine(FadeAlpha(spriteToFade, 1.0f, fadeDuration));

            // 2. 移動処理 (nullチェックを徹底)
            if (objectToMove2 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove2, initialY2, moveDuration));
            }
            if (objectToMove3 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove3, initialY3, moveDuration));
            }
            // ★ 追加: 4つ目の移動画像
            if (objectToMove4 != null)
            {
                StartCoroutine(MoveToHeight(objectToMove4, initialY4, moveDuration));
            }

            // 3. スコアテキストの処理 (nullチェックを徹底)
            if (scoreTextTransform != null && scoreTextComponent != null)
            {
                StartCoroutine(MoveToHeight(scoreTextTransform, initialScoreTextY, moveDuration));
                // 移動完了を待たずに非表示にする場合はこちら。
                // より厳密には、MoveToHeightコルーチンの後にSetActive(false)を実行すべきですが、
                // 現状のOFF処理は並行実行なので、これで良いとしています。
                scoreTextComponent.gameObject.SetActive(false);
            }
        }
    }


    // 1. Spriteの透明度を指定値まで滑らかに変化させるコルーチン (変更なし)
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


    // 2. オブジェクトを指定したY座標に滑らかに移動させるコルーチン (変更なし)
    private IEnumerator MoveToHeight(Transform trans, float targetY, float duration)
    {
        // ★ 修正点: nullチェックを追加
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