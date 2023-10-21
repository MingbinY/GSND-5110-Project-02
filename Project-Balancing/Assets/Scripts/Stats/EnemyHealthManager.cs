using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : BasicHealthManager
{
    public GameObject deathSFX;
    public GameObject ammoDrop;
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
        if(deathSFX) Instantiate(deathSFX, transform.position, Quaternion.identity);
        if (ammoDrop) Instantiate(ammoDrop, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
        gameObject.SetActive(false);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        BroadcastMessage("HitBlink");
    }
}
