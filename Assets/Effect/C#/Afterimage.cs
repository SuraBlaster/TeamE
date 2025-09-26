//using UnityEngine;

//public class Afterimage : MonoBehaviour
//{
//    public float lifeTime = 0.5f; // 残像が消えるまでの時間
//    private SpriteRenderer sr;
//    private Color color;

//    void Start()
//    {
//        sr = GetComponent<SpriteRenderer>();
//        color = sr.color;
//        Destroy(gameObject, lifeTime); // 一定時間後に自動削除
//    }

//    void Update()
//    {
//        color.a -= Time.deltaTime / lifeTime; // アルファ値を徐々に減少
//        sr.color = color;
//    }
//}

using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float lifeTime = 0.2f; // 残像の生存時間
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

        // 経過時間を加算
        timer += Time.deltaTime;

        // アルファ値を 1 → 0 に補間
        color.a = Mathf.Lerp(1f, 0f, timer / lifeTime);
        sr.color = color;

        // 寿命を超えたら削除
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}

