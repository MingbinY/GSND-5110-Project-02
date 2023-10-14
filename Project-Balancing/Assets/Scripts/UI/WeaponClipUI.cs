using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponClipUI : MonoBehaviour
{
    public RaycastWeapon weapon { get; private set; }
    [SerializeField] GameObject clipUI;
    [SerializeField] GameObject reloadUI;

    public TMP_Text currentClipText;

    private void Start()
    {
        weapon = GetComponentInParent<RaycastWeapon>();
        if (!clipUI || !currentClipText) Destroy(gameObject);
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
