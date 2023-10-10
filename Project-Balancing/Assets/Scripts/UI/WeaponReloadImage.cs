using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReloadImage : MonoBehaviour
{
    [SerializeField] WeaponClipUI weaponUI;
    Image image;

    int targetAmount = 0;
    float reloadTime = 5;
    float startTime = 0;

    private void OnEnable()
    {
        reloadTime = weaponUI.weapon.weaponStats.reloadTime;
        image.fillAmount = 1;
        startTime = Time.time;
    }
    private void Awake()
    {
        weaponUI = GetComponentInParent<WeaponClipUI>();
        if (!weaponUI)
            Destroy(gameObject);
        image = GetComponent<Image>();
    }

    private void Update()
    {
        float fillAmount = 1 - ((Time.time - startTime) / reloadTime);
        image.fillAmount = fillAmount;
    }


}
