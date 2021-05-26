using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public float Angle = 1f;
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, Angle * Time.deltaTime, 0.0f));
    }
}
