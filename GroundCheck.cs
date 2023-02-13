using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SendMessageUpwards("ResetGravity");
            Debug.Log("Hit Ground");
        }
    }
}
