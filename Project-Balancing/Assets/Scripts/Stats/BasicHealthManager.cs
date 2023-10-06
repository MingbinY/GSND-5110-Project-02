using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public float regenerateRate = 0f;
    
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
    public virtual void Death() { Destroy(gameObject); }
    public virtual void TakeDamage(int damage) { 
        Debug.Log(gameObject.name + "Taking Damage of " + damage);
        health -= damage;
    }
}
