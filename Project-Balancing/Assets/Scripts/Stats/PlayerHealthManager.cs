using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : BasicHealthManager
{
    public int armor = 100;
    public float armorDeductionRate = 0.5f;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            int sharedDamage = (int)(damage * armorDeductionRate);
            armor -= sharedDamage;
            health -= (damage - sharedDamage);
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }
}
