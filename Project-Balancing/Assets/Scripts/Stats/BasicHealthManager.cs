using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public float regenerateRate = 0f;

    public virtual void Death() { }
    public virtual void TakeDamage(int damage) { }
}
