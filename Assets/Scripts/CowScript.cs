using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class CowScript : BaseSpawnPickUp
{
    public override void Interact(Transform playerPickup)
    {
        if (playerPickup.childCount > 0)
        {
            if (playerPickup.GetChild(0).CompareTag("Bucket"))
            {
                foreach(Transform child in playerPickup)
                {
                    Destroy(child.gameObject);
                }
                Instantiate(objectPrefab, playerPickup);
            }

        }
    }
}
