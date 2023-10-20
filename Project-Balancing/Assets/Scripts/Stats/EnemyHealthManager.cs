using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : BasicHealthManager
{
    public GameObject deathSFX;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Death()
    {
        base.Death();
        Instantiate(deathSFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        BroadcastMessage("HitBlink");
    }
}
