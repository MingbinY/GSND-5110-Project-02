using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : BasicHealthManager
{
    public int armor = 100;
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
    }

    public override void TakeDamage(int damage)
    {
        if (armor > damage)
        {
            armor -= damage;
        }
        else
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }

        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }
}
