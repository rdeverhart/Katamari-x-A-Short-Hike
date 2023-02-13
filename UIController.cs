using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Canvas UI;
    public Image[] Feathers;
    public Image Size;
    public GameObject MovementController;
    int birdCount;
    int prevBird;
    float CurrentRadius;
    public TextMeshProUGUI RadiusText;

    private void Update()
    {

            birdCount = MovementController.GetComponent<PlayerMovement>().numBirds;
            for (int i = 0; i < Feathers.Length; i++)
            {
                if (i < birdCount) { 
                Feathers[i].enabled = true;
                }
                else
                {
                Feathers[i].enabled = false;
                }
            }

            CurrentRadius = MovementController.GetComponent<PlayerMovement>().SizeTotal;
        Size.transform.localScale = new Vector3 (CurrentRadius*0.01f, CurrentRadius * 0.01f, 1.0f);

        RadiusText.text = "Radius: " + CurrentRadius;


    }



}
