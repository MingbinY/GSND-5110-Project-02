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
    GameObject shotgun;
    Vector3 weaponSlotOffset;
    
    bool isFiring = false;

    private void Awake()
    {
        weaponSlotOffset = weaponSlot.transform.localPosition;
        playerManager = GetComponent<PlayerManager>();
        pistol = Instantiate(startingWeapons[0], weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        shotgun = Instantiate(startingWeapons[1], weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        EquipWeapon(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);

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

    public void EquipWeapon(int index)
    {
        //secondaryWeapon.SetActive(false);
        if (index == 0)
        {
            shotgun.SetActive(false);
            pistol.SetActive(true);
            currentWeapon = pistol.GetComponent<RaycastWeapon>();
        }

        if (index == 1)
        {
            shotgun.SetActive(true);
            pistol.SetActive(false);
            currentWeapon = shotgun.GetComponent<RaycastWeapon>();
        }
       
        weaponSlot.transform.localPosition = weaponSlotOffset + currentWeapon.weaponStats.weaponOffset;
    }
}
