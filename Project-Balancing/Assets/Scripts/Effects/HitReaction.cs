using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{
    Material material;
    MeshRenderer meshRenderer;
    Color defaultColor;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        defaultColor = material.color;
    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) +1.0f;
        meshRenderer.material.color = defaultColor * intensity;
    }

    public void HitBlink()
    {
        blinkTimer = blinkDuration;
    }
}
