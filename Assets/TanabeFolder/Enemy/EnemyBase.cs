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
    protected float move_speed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected Vector2 ToPlayer() { return player_transform.position - transform.position; }
    public void SetPlayerTransform(Transform transform) { player_transform = transform; }
    public void SetManager(EnemyManager manager) { this.manager = manager; }
}
