using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 5.0f;
    [SerializeField]
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float axiz_x = 0.0f, axiz_y = 0.0f;

        //Y軸移動
        if (Input.GetKey(KeyCode.W))
            axiz_y += 1.0f;
        else if (Input.GetKey(KeyCode.S))
            axiz_y -= 1.0f;

        //X軸移動
        if (Input.GetKey(KeyCode.A))
            axiz_x -= 1.0f;
        else if (Input.GetKey(KeyCode.D))
            axiz_x += 1.0f;

        //アニメーターに設定
        if (axiz_y == 0.0f && axiz_x == 0.0f)
            animator.SetBool("MoveFlg", false);
        else
            animator.SetBool("MoveFlg", true);

        // オブジェクトを移動させる
        Vector2 movement = new Vector2(axiz_x, axiz_y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // オブジェクトを反転させる
        if(axiz_x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(axiz_x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }


}

