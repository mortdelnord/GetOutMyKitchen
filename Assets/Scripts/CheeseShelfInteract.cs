using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheeseShelfInteract : BaseSpawnPickUp
{
    private CheeseShelf cheeseShelf;
    private void Start()
    {
        cheeseShelf = gameObject.GetComponent<CheeseShelf>();
    }
    public override void Interact(Transform playerPickup)
    {
        if (playerPickup.childCount > 0)
        {
            if (playerPickup.GetChild(0).CompareTag("Cheese"))
            {
                cheeseShelf.FillShelf(objectPrefab);
                foreach (Transform child in playerPickup)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
