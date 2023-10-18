using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public bool isDead { get; private set; }

    public float regenerateRate = 0f;

    [Tooltip("For Testing Purpose")]
    public bool isInvincible = false;

    public virtual void Start()
    {
        health = maxHealth;
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }
    public virtual void Death() { if (isDead) return; isDead = true; }
    public virtual void TakeDamage(int damage) {
        if (isDead) return;
        if (isInvincible) return;
        Debug.Log(gameObject.name + "Taking Damage of " + damage);
        health -= damage;
    }
}
