using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int health = 30;

    public override void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() == null)
            return;

        PlayerHealthManager healthManager = other.GetComponent<PlayerHealthManager>();
        if (healthManager != null)
        {
            healthManager.health += health;
            Destroy(gameObject);
        }
    }
}
