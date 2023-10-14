using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public GameObject weapon;
    public override void OnPickup()
    {
        base.OnPickup();
        PlayerWeaponSlot weaponSlot = FindObjectOfType<PlayerWeaponSlot>();
        if (weaponSlot)
        {
        }
    }
}
