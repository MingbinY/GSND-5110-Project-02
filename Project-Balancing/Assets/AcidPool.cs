using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    public int damage = 5;
    public float damageInterval = 3f;
    float nextDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHealthManager>() != null)
        {
            PlayerHealthManager playerHealth = other.GetComponent<PlayerHealthManager>();
            if (Time.time > nextDamageTime)
            {
                playerHealth.TakeDamage(damage);
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}
