using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katamari : MonoBehaviour
{
    float currentRadius;
    float objectRadius;
    public GameObject hitObject;
    public float size;
    public int ID;
    public bool IsBird = false;
    public bool pickupEnabled = true;
    string ThisObjname;
    
    private void Awake()
    {
        gameObject.name = GetInstanceID().ToString();
        ThisObjname = gameObject.name;
        ID = GetInstanceID();
        
    }
    void OnTriggerEnter(Collider collision)
    {
        hitObject = collision.gameObject;
       //if it's the katamari being hit, it can be picked up, and it isn't too big
        if(collision.gameObject.tag == "Katamari")
        {
            if (size <= hitObject.GetComponentInParent<PlayerMovement>().MaxSize)
            {
                if (pickupEnabled == true)
                {
                    //attach it to the player
                    this.transform.parent = hitObject.transform;
                    hitObject.transform.parent.GetComponent<PlayerMovement>().SetRadius(size);
                    hitObject.transform.parent.GetComponent<PlayerMovement>().AddToList(gameObject);
                    
                }
                
             }
            else
            {
                //otherwise bounce it off
                hitObject.transform.parent.GetComponent<PlayerMovement>().BounceOff(gameObject);

            }
        }

    }
    

    
}
