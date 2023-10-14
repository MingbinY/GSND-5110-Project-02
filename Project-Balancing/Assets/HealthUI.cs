using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    TMP_Text healthText;
    PlayerHealthManager playerHealth;
    
    private void Awake()
    {
        healthText = GetComponent<TMP_Text>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();

        healthText.text = playerHealth.health.ToString();
    }

    private void LateUpdate()
    {
        healthText.text = playerHealth.health.ToString();
    }
}
