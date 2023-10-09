using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : BasicHealthManager
{
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
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}
