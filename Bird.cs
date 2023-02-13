using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Katamari"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            GetComponent<Katamari>().pickupEnabled = true;
            BoxCollider thisCollider = GetComponent<BoxCollider>();
            thisCollider.isTrigger = false;
        } 
    }
}
