using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : EnemyBase
{
    private float current_time = 0.0f;
    [SerializeField]
    private float stop_timer = 1.0f;
    [SerializeField]
    private float move_timer = 0.5f;
    [SerializeField]
    private Animator animator;

    public override string GetName() { return "Frog"; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Death();

        current_time += Time.deltaTime;
        animator.SetBool("IsMove", false);
        if (current_time > move_timer)
        {
            animator.SetBool("IsMove", true);
            Vector2 distance = ToPlayer();
            Vector2 movement = distance.normalized * move_speed * Time.deltaTime;
            transform.Translate(movement);
            LeftorRight(movement.x);
            if (current_time > move_timer + stop_timer)
            {
                current_time = 0.0f;
            }
        }
    }
}
