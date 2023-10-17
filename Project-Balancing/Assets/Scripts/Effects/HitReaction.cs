using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{
    Material material;
    Color originColor;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        
    }

    public void HitBlink()
    {
        material.color = Color.white;
    }
}
