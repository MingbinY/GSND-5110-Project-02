using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammo = 30;

    public override void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() == null)
            return;

        RaycastWeapon weapon = other.GetComponentInChildren<RaycastWeapon>();
        weapon.currentClip += ammo;

        Destroy(gameObject);
    }
}
