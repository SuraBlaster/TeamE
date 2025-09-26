using UnityEngine;
using UnityEngine.UI; // UIを使うために必要

public class BlinkingText : MonoBehaviour
{
    public float blinkSpeed = 2f;    // 点滅の速さ
    public float minAlpha = 0.2f;    // 最小の透明度 (0.0fで完全に透明, 1.0fで完全に不透明)
    public float maxAlpha = 1.0f;    // 最大の透明度

    private Text textComponent;

    void Start()
    {
        // Textコンポーネントを取得
        textComponent = GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.LogError("Text component not found!");
            enabled = false; // コンポーネントがなければスクリプトを無効化
            return;
        }
    }

    void Update()
    {
        // 時間とSin関数を使って-1.0から1.0の間を滑らかに変化する値を取得
        float rawAlpha = Mathf.Sin(Time.time * blinkSpeed);

        // rawAlphaを0.0から1.0の範囲に正規化
        // (Sin関数は-1から1なので、(値 + 1) / 2 で0から1の範囲に変換)
        float normalizedAlpha = (rawAlpha + 1f) / 2f;

        // 最小アルファと最大アルファの間で線形補間
        float finalAlpha = Mathf.Lerp(minAlpha, maxAlpha, normalizedAlpha);

        // 現在の色を取得
        Color newColor = textComponent.color;

        // A（アルファ）値のみを設定 (0.0fから1.0fの範囲で)
        newColor.a = finalAlpha;

        // テキストに新しい色を設定
        textComponent.color = newColor;
    }
}