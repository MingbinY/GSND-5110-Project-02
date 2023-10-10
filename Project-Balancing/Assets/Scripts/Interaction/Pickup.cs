using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Interactable interactable;
    public virtual void Awake()
    {
        interactable = GetComponent<Interactable>();
        if (!interactable)
        {
            interactable = gameObject.AddComponent<Interactable>();
        }

        interactable.onInteract.AddListener(OnPickup);
    }

    public virtual void OnPickup()
    {

    }
}
