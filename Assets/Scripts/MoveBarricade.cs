using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarricade : MonoBehaviour
{
    private Vector3 currentPosition;
    void Start()
    {
        currentPosition = transform.position;
    }

    public void MoveTheBarricade()
    {
        currentPosition.x = 0 - currentPosition.x;
        transform.position = currentPosition;
    }

}
