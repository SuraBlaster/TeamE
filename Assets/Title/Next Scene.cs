//using UnityEngine;
//using UnityEngine.SceneManagement; // シーン管理のために必要

//public class KeySceneChanger : MonoBehaviour
//{
//    // 遷移先のシーン名をInspectorで設定できるようにします
//    public string targetSceneName = "GameScene";

//    void Update()
//    {
//        // 特定のキー（ここではSpaceキー）が押された瞬間を検出
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            // 設定されたシーン名に遷移
//            SceneManager.LoadScene(targetSceneName);
//        }
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalInputSceneChanger : MonoBehaviour
{
    // 遷移先のシーン名をInspectorで設定
    public string targetSceneName = "GameScene";

    void Update()
    {
        // --- (1) キーボードの任意のキーを検出 ---
        // どのキーでも押された瞬間に true になります
        bool keyPressed = Input.anyKeyDown;

        // --- (2) マウスの左クリックを検出 ---
        // マウスの左ボタンが押された瞬間に true になります (0 = 左ボタン)
        bool mouseClicked = Input.GetMouseButtonDown(0);

        // どちらかの入力があった場合、シーン遷移を実行
        if (keyPressed || mouseClicked)
        {
            LoadTargetScene();
        }
    }

    // シーン遷移を実行するメソッド
    void LoadTargetScene()
    {
        // 遷移先のシーン名が設定されているか確認
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