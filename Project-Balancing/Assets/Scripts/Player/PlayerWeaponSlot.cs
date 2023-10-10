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
    GameObject primaryWeapon, secondaryWeapon;
    Vector3 weaponSlotOffset;
    
    bool isFiring = false;

    private void Awake()
    {
        weaponSlotOffset = weaponSlot.transform.localPosition;
        playerManager = GetComponent<PlayerManager>();
        primaryWeapon = Instantiate(startingWeapons[0], weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        secondaryWeapon = Instantiate(startingWeapons[1], weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        SwitchWeapon(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(true);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(false);
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

    public void SwitchWeapon(bool primary)
    {
        if (primary)
        {
            secondaryWeapon.SetActive(false);
            primaryWeapon.SetActive(true);
            currentWeapon = primaryWeapon.GetComponent<RaycastWeapon>();
        }
        else
        {
            primaryWeapon.SetActive(false);
            secondaryWeapon.SetActive(true);
            currentWeapon = secondaryWeapon.GetComponent<RaycastWeapon>();
        }

        weaponSlot.transform.localPosition = weaponSlotOffset + currentWeapon.weaponStats.weaponOffset;
    }

    public void EquipWeapon(GameObject weapon)
    {
        RaycastWeapon raycastWeapon = weapon.GetComponent<RaycastWeapon>();

        if (raycastWeapon != null)
        {
            if (raycastWeapon.isPrimary)
            {
                Destroy(primaryWeapon);
                primaryWeapon = Instantiate(weapon, weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
            }
            else
            {
                Destroy(secondaryWeapon);
                secondaryWeapon = Instantiate(weapon, weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
            }
        }
        SwitchWeapon(raycastWeapon.isPrimary ? true : false);
    }
}
