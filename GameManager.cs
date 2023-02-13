using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public GameObject[] pickupObjects;
    // Start is called before the first frame update
    private void Awake()
    {
        GM = this;
    }
    void Start()
    {
        pickupObjects = GameObject.FindGameObjectsWithTag("Pickup");
        Debug.Log(pickupObjects.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
