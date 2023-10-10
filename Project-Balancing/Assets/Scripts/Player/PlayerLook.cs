using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Tooltip("The root object of the camera")]
    public GameObject camRoot;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public Transform raycastHitTransform;
    RaycastHit hit;

    public void HandleLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        camRoot.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Left and Right looking
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    private void Update()
    {
        if (Physics.Raycast(camRoot.transform.position, camRoot.transform.forward, out hit))
        {
            raycastHitTransform = hit.transform;
        }
    }
}
