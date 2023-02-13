using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float SpeedMult = 1;
    public GameObject KatamariBall;
    public SphereCollider KatamariCollider;
    float layerSize = 0;
    float nextSize = 5;
    public float MaxSize = 1;
    GameObject tooBigObj;
    public List<GameObject> stuckObjects = new List<GameObject>();
    public int numBirds = 0;
    int MaxFlaps = 0;
    public Camera MainCamera;
    public float SizeTotal = 5;


    void Start()
    {
        Physics.gravity = new Vector3(0.0f, -50f, 0.0f);
        rb = GetComponent<Rigidbody>();
        KatamariCollider = GetComponentInChildren<SphereCollider>();

    }

    //"Flap" the Katmari up until all birds are used up
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (numBirds > 0)
            {
                rb.AddForce(new Vector3(0, 2000, 0));
                numBirds--;
                
            }
        }
    }
    //move player, ball spins with input
    void FixedUpdate()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        rb.AddForce(-moveVert * SpeedMult, 0f, moveHoriz * SpeedMult);
        KatamariBall.transform.Rotate((rb.velocity.z/3) * (SpeedMult * .066f), 0f, (-rb.velocity.x/3) * (SpeedMult * .066f), Space.World);

        
    }
    //Reset all "Flap" charges upon touching the ground
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Physics.gravity = new Vector3(0.0f, -50f, 0.0f);
            numBirds = MaxFlaps;
            
        }
    }

    //Keeps track of radius of the katamari. once it hits a certain threshold, it will increase in size
    public void SetRadius(float size)
    {
        //adds object size to total size of the current layer when called
        layerSize += size;
        SizeTotal += size;

        //if the size is enough to reach the next threshold, displace the katamari accordingly
        if (layerSize >= nextSize)
        {
            KatamariCollider.radius += size * .05f;
            layerSize = 0;
            nextSize *= 1.5f;
            MaxSize += 1;
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x+1f, MainCamera.transform.position.y+1f, MainCamera.transform.position.z);

        }


    }
    //Keeps track of what objects are attached to the katamari
    public void AddToList(GameObject StuckObject)
    {
        //check every item in the scene to see if it was the one being hit
        for (int i = 0; i < GameManager.GM.pickupObjects.Length; i++)
        {
            if (StuckObject.GetComponent<Katamari>().ID == GameManager.GM.pickupObjects[i].GetComponent<Katamari>().ID)
            {
                //add it to the list if it is
                stuckObjects.Add(GameManager.GM.pickupObjects[i]);
                if (StuckObject.GetComponent<Katamari>().IsBird == true)
                {
                    numBirds++;
                    MaxFlaps++;
                    Physics.gravity = new Vector3(0.0f, -(50/(numBirds*1.005f)), 0.0f);
                    StuckObject.GetComponent<Katamari>().pickupEnabled = false;
                }
            }   
        }
    }

    public void BounceOff(GameObject TooBigObj)
    {
        tooBigObj = TooBigObj;
        
        //Set the direction of the player's ball in relation to the object collided with
        Vector3 ObjDir = new Vector3(TooBigObj.transform.position.x - KatamariBall.transform.position.x, 0.0f, TooBigObj.transform.position.z - KatamariBall.transform.position.z).normalized;
        rb.AddForce(-ObjDir*700);
        if (TooBigObj.GetComponent<Katamari>().pickupEnabled == true)
        {
            StartCoroutine(WaitEnableCollision());
            

        }
        
        
    }

    IEnumerator WaitEnableCollision()
    {
        //detatch 1/3 of the objects on the ball and prevent them from being picked up temporarily. Also remove their size from the total size
        int objectsToDrop = (int)(stuckObjects.Count * 0.66f);
        for (int i = objectsToDrop; i < (stuckObjects.Count - 1); i++)
        {
            stuckObjects[i].GetComponent<Rigidbody>().isKinematic = false;
            stuckObjects[i].GetComponent<MeshCollider>().isTrigger = false;
            stuckObjects[i].transform.parent = null;

            tooBigObj.GetComponent<Katamari>().pickupEnabled = false;
            SizeTotal -= stuckObjects[i].GetComponent<Katamari>().size;

        }


        //If the object that was too big was a bird, knock it down.
        if (tooBigObj.GetComponent<Katamari>().IsBird)
        {
            tooBigObj.GetComponent<MeshCollider>().isTrigger = false;
            tooBigObj.GetComponent<Rigidbody>().isKinematic = false;
            tooBigObj.GetComponent<Katamari>().pickupEnabled = false;
        }

        //After one second, reset all the collison parameters for affected objects
        yield return new WaitForSeconds(1f);

        if (tooBigObj.GetComponent<Katamari>().IsBird)
        {
            tooBigObj.GetComponent<MeshCollider>().isTrigger = true;
            tooBigObj.GetComponent<Rigidbody>().isKinematic = true;
            tooBigObj.GetComponent<Katamari>().pickupEnabled = true;
        }
        //re-enable collision Params
        for (int i = objectsToDrop; i < (stuckObjects.Count - 1); i++)
        {
            stuckObjects[i].GetComponent<Rigidbody>().isKinematic = true;
            stuckObjects[i].GetComponent<MeshCollider>().isTrigger = true;
            tooBigObj.GetComponent<Katamari>().pickupEnabled = true;        
        }
        for (int i = objectsToDrop; i < (stuckObjects.Count - 1); i++)
        {
            stuckObjects.Remove(stuckObjects[i]);
            layerSize -= 1;
        }
    }


}
