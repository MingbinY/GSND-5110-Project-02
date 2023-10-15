using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public Vector3 rotationValue = Vector3.zero;

    private void Update()
    {
        transform.Rotate(rotationValue * Time.deltaTime);
    }
}
