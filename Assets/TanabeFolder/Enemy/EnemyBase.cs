using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private EnemyManager manager;

    [SerializeField]
    private Transform player_transform;

    [SerializeField]
    private float move_speed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    protected void MoveToPlayer()
    {
        Vector2 distance = player_transform.position- transform.position;

        // オブジェクトを移動させる
        Vector2 movement = distance.normalized * move_speed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void SetPlayerTransform(Transform transform) { player_transform = transform; }
    public void SetManager(EnemyManager manager) { this.manager = manager; }
}
