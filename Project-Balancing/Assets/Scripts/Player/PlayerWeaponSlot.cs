using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlot : MonoBehaviour
{
    public GameObject weaponSlot;
    public RaycastWeapon currentWeapon;
    public PlayerManager playerManager;
    public List<GameObject> startingWeapons;
    GameObject pistol;
    Vector3 weaponSlotOffset;
    
    bool isFiring = false;

    private void Awake()
    {
        weaponSlotOffset = weaponSlot.transform.localPosition;
        playerManager = GetComponent<PlayerManager>();
        pistol = Instantiate(startingWeapons[0], weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        EquipWeapon();
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

    public void EquipWeapon()
    {
        //secondaryWeapon.SetActive(false);
        pistol.SetActive(true);
        currentWeapon = pistol.GetComponent<RaycastWeapon>();

        weaponSlot.transform.localPosition = weaponSlotOffset + currentWeapon.weaponStats.weaponOffset;
    }
}
