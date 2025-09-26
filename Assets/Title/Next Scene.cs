////using UnityEngine;
////using UnityEngine.SceneManagement; // シーン管理のために必要

////public class KeySceneChanger : MonoBehaviour
////{
////    // 遷移先のシーン名をInspectorで設定できるようにします
////    public string targetSceneName = "GameScene";

////    void Update()
////    {
////        // 特定のキー（ここではSpaceキー）が押された瞬間を検出
////        if (Input.GetKeyDown(KeyCode.Space))
////        {
////            // 設定されたシーン名に遷移
////            SceneManager.LoadScene(targetSceneName);
////        }
////    }
////}

//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class UniversalInputSceneChanger : MonoBehaviour
//{
//    // 遷移先のシーン名をInspectorで設定
//    public string targetSceneName = "GameScene";

//    void Update()
//    {
//        // --- (1) キーボードの任意のキーを検出 ---
//        // どのキーでも押された瞬間に true になります
//        bool keyPressed = Input.anyKeyDown;

//        // --- (2) マウスの左クリックを検出 ---
//        // マウスの左ボタンが押された瞬間に true になります (0 = 左ボタン)
//        bool mouseClicked = Input.GetMouseButtonDown(0);

//        // どちらかの入力があった場合、シーン遷移を実行
//        if (keyPressed || mouseClicked)
//        {
//            LoadTargetScene();
//        }
//    }

//    // シーン遷移を実行するメソッド
//    void LoadTargetScene()
//    {
//        // 遷移先のシーン名が設定されているか確認
//        if (!string.IsNullOrEmpty(targetSceneName))
//        {
//            SceneManager.LoadScene(targetSceneName);
//        }
//        else
//        {
//            Debug.LogError("遷移先のシーン名が設定されていません。Inspectorを確認してください。");
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;           // Imageコンポーネントを使うために必要
using UnityEngine.SceneManagement;
using System.Collections;       // Coroutineを使うために必要

public class UniversalInputSceneChanger : MonoBehaviour
{
    // 遷移先のシーン名をInspectorで設定
    public string targetSceneName = "GameScene";

    // Inspectorで「Next Scene」の子のImageコンポーネントを設定
    public Image fadeImage;

    // フェードアウトにかける時間（秒）
    public float fadeDuration = 0.5f;

    void Update()
    {
        // 既に遷移処理が始まっている場合は、入力を無視
        if (fadeImage.color.a >= 1f) return;

        // キーボードの任意のキー、またはマウスの左クリックを検出
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            // 入力が検出されたら、コルーチンを開始してフェードアウトとシーン遷移を実行
            StartCoroutine(FadeAndLoadScene());
        }
    }

    // フェードアウトとシーン遷移を順次実行するコルーチン
    private IEnumerator FadeAndLoadScene()
    {
        if (fadeImage == null)
        {
            Debug.LogError("Fade Imageが設定されていません。Inspectorを確認してください。");
            // 画像がない場合はすぐに遷移
            SceneManager.LoadScene(targetSceneName);
            yield break; // コルーチンを終了
        }

        // 初期カラー（現在の透明度）を取得
        Color currentColor = fadeImage.color;
        float startTime = Time.time;

        // フェードアウト処理（透明度を0から1へ増加）
        while (Time.time < startTime + fadeDuration)
        {
            // 経過時間に基づいて0.0から1.0の値を取得
            float t = (Time.time - startTime) / fadeDuration;

            // アルファ値を lerp（線形補間）で滑らかに変化させる
            currentColor.a = Mathf.Lerp(0f, 1f, t);
            fadeImage.color = currentColor;

            yield return null; // 1フレーム待機
        }

        // 完全に不透明になったことを保証
        currentColor.a = 1f;
        fadeImage.color = currentColor;

        // シーン遷移を実行
        LoadTargetScene();
    }

    // シーン遷移を実行するメソッド
    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("遷移先のシーン名が設定されていません。Inspectorを確認してください。");
        }
    }
}