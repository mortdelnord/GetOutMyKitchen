using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketPileScript : BaseSpawnPickUp
{
    public override void Interact(Transform playerPickup)
    {
        if(playerPickup.childCount > 0)
        {
            return;
        }else
        {
            Instantiate(objectPrefab, playerPickup);
        }
    }
}
