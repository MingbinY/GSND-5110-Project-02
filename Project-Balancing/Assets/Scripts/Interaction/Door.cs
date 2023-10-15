using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isLocked)
            return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLocked)
            return;
    }


}
