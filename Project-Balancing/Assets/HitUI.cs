using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUI : MonoBehaviour
{
    [Range(0,1)] public float blinkAlpha;
    public float blinkDuration;
    float blinkTimer;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void PlayerHitReaction()
    {
        blinkTimer = blinkDuration;
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float alpha = (lerp * blinkAlpha);
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        Debug.Log("Alpha " + alpha);
    }
}
