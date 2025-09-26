using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeel : Weapon
{
    public float heel_amount = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.GetComponent<Health>().TakeHeel(heel_amount);
        Destroy(gameObject);
    }

    protected override void Fire() { }
}
