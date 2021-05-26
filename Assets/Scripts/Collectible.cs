using System;//.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action<Collectible> OnPickup;
    public float turnSpeed = 15f;
    private void Start()
    {
        ChangeColor(false);
    }

    public void ChangeColor(bool isRed)
    {
        if(isRed)
        {
            transform.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            transform.GetComponent<Renderer>().material.color = Color.yellow;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, turnSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<AIController>();
        if(player !=null)
        {
            OnPickup?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

}
