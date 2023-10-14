using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmorUI : MonoBehaviour
{
    TMP_Text armorText;
    PlayerHealthManager playerHealth;

    private void Awake()
    {
        armorText = GetComponent<TMP_Text>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();

        armorText.text = playerHealth.armor.ToString();
    }

    private void LateUpdate()
    {
        armorText.text = playerHealth.armor.ToString();
    }
}
