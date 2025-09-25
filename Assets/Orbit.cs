using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform player;    
    public float radius = 2f;   
    public float speed = 2f;    
    public float startAngle = 0f; 

    private float angle; 

    void Start()
    {
        angle = startAngle * Mathf.Deg2Rad;
    }

    void Update()
    {
        if (player == null) return;

        angle += speed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = player.position + new Vector3(x, y, 0);
    }
}
