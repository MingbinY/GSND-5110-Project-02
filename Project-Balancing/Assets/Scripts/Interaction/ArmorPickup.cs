using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPickup : Pickup
{
    public int armor = 30;

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
            healthManager.armor += armor;
            Destroy(gameObject);
        }
    }
}
