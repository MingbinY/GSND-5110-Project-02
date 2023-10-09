using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    RaycastWeapon weapon;
    [SerializeField] GameObject clipUI;
    [SerializeField] GameObject reloadUI;

    public TMP_Text currentClipText;
    public TMP_Text maxClipText;

    private void Start()
    {
        weapon = GetComponentInParent<RaycastWeapon>();
        if (!clipUI || !reloadUI || !currentClipText || !maxClipText) Destroy(gameObject);

        maxClipText.text = weapon.weaponStats.clipSize.ToString();
    }
    private void Update()
    {
        currentClipText.text = weapon.currentClip.ToString();
    }

    public void ActiveReloadUI()
    {
        clipUI.SetActive(false);
        reloadUI.SetActive(true);
    }

    public void DeactiveReloadUI()
    {
        reloadUI.SetActive(false);
        clipUI.SetActive(true);
    }
}
