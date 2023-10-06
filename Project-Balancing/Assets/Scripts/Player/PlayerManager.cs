using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerLocomotion locomotion;
    public PlayerLook look;
    public PlayerWeaponSlot weaponSlot;

    private void Start()
    {
        locomotion = GetComponent<PlayerLocomotion>();
        look = GetComponent<PlayerLook>();
        weaponSlot = GetComponent<PlayerWeaponSlot>();
    }
}
