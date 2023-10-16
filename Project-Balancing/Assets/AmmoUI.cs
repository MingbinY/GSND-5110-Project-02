using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    TMP_Text ammoText;
    PlayerWeaponSlot weaponSlot;

    private void Awake()
    {
        ammoText = GetComponent<TMP_Text>();
        weaponSlot = FindObjectOfType<PlayerWeaponSlot>();
    }

    private void LateUpdate()
    {
        ammoText.text = weaponSlot.currentWeapon.currentClip.ToString();
    }
}
