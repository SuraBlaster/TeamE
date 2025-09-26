using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int baseDamage = 10;  // Šî‘bƒ_ƒ[ƒW—Ê
    public float fireRate = 1f;     // ”­ËŠÔŠu(•b)

    private float fireTimer;

    public virtual void Initialize(PlayerController playerController, int damage, float rate)
    {
        baseDamage = damage;
        fireRate = rate;
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Fire();
        }
    }

    // ”h¶ƒNƒ‰ƒX‚²‚Æ‚ÌUŒ‚ˆ—
    protected abstract void Fire();
}
