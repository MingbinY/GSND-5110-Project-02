using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public float movementDistance = 1f;
    public float returnSpeed = 2f;
    public float snappiness = 0.1f;

    public Vector3 orginalPos;
    public Vector3 targetPos;
    public Vector3 currentPos;

    PlayerLocomotion locomotion;

    private void Awake()
    {
        orginalPos = transform.localPosition;
        locomotion = FindObjectOfType<PlayerLocomotion>();
    }
    private void Update()
    {
        if (locomotion.characterController.velocity.magnitude > 0)
        {
            transform.localPosition += new Vector3(Random.Range(-movementDistance, movementDistance), 0, 0);
        }
        targetPos = Vector3.Lerp(targetPos, Vector3.zero, returnSpeed * Time.deltaTime);
        currentPos = Vector3.Slerp(currentPos, targetPos, snappiness * Time.fixedDeltaTime);
        transform.localPosition = currentPos;
    }
}
