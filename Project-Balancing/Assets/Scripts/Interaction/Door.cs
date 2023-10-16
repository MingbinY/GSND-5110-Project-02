using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;

    [SerializeField] GameObject movingPart;
    [SerializeField] Vector3 closePosition;
    [SerializeField] Vector3 openPosition;
    [SerializeField] Quaternion closeRotation;
    [SerializeField] Quaternion openRotation;

    bool isOpen = false;
    [SerializeField] float moveSpeed = 1f;
    private void Awake()
    {
        closePosition = movingPart.transform.localPosition;
        closeRotation = movingPart.transform.rotation;
    }

    private void Update()
    {
       if (isOpen)
        {
            movingPart.transform.localPosition = Vector3.Lerp(movingPart.transform.localPosition, openPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            movingPart.transform.localPosition = Vector3.Lerp(movingPart.transform.localPosition, closePosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLocked || !other.GetComponent<PlayerManager>())
            return;

        isOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLocked || !other.GetComponent<PlayerManager>())
            return;

        isOpen = false;
    }
}
