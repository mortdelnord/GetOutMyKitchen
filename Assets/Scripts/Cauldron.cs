using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : BaseSpawnPickUp
{

    private bool doneCooking = false;
    private bool isCooking = false;
    private float cookTimer = 0f;
    public float cookTimerMax;
    private int milkAmount;
    private int milkNeeded;
    public override void Interact(Transform playerPickup)
    {
        if(playerPickup.childCount > 0)
        {
            if (playerPickup.GetChild(0).CompareTag("Milk") && !isCooking)
            {
                foreach (Transform child in playerPickup)
                {
                    Destroy(child.gameObject);
                }
                isCooking = true;
                //Cooking();
            }
        }else
        {
            if (doneCooking)
            {
                Instantiate(objectPrefab, playerPickup);
                doneCooking = false;
            }
        }
    }

    private void Update()
    {
        if (isCooking)
        {
            cookTimer += Time.deltaTime;
            if (cookTimer >= cookTimerMax)
            {
                cookTimer = 0f;
                Debug.Log("Done Cooking");
                isCooking = false;
                doneCooking = true;
            }
        }
    // }
    // private void Cooking()
    // {
    //     while(isCooking)
    //     {
    //         cookTimer += Time.deltaTime;
    //         if (cookTimer >= cookTimerMax)
    //         {
    //             cookTimer = 0f;
    //             Debug.Log("Done Cooking");
    //             isCooking = false;
    //             doneCooking = true;
    //         }
    //     }
    }


    
}
