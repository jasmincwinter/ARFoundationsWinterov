using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject character; 

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        character.transform.Translate(Vector3.forward * Time.deltaTime * 0.07f);
    }
}
