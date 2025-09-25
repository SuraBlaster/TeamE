using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : EnemyBase
{
    public override string GetName() { return "Spider"; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        Vector2 distance = ToPlayer();
        Vector2 movement = distance.normalized * move_speed * Time.deltaTime;
        transform.Translate(movement);
        LeftorRight(movement.x);
    }
}
