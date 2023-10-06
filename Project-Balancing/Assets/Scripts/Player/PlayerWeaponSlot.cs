using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlot : MonoBehaviour
{
    public GameObject weaponSlot;
    public RaycastWeapon currentWeapon;
    public PlayerManager playerManager;
    
    bool isFiring = false;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        currentWeapon = weaponSlot.GetComponentInChildren<RaycastWeapon>();
    }

    private void Update()
    {
        if (isFiring)
        {
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Fire();
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring= false;
    }
}
