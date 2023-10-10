using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerLocomotion locomotion;
    public PlayerLook look;
    public PlayerWeaponSlot weaponSlot;
    public Recoil recoilScript;

    private void Awake()
    {
        locomotion = GetComponent<PlayerLocomotion>();
        look = GetComponent<PlayerLook>();
        weaponSlot = GetComponent<PlayerWeaponSlot>();
        recoilScript = GetComponentInChildren<Recoil>();recoilScript = GetComponentInChildren<Recoil>();
    }
}
