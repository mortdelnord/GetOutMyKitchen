using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheeseShelfInteract : BaseSpawnPickUp
{
    private CheeseShelf cheeseShelf;
    public Animator playerAnimator;

    Transform pickup;
    private void Start()
    {
        cheeseShelf = gameObject.GetComponent<CheeseShelf>();
    }
    public override void Interact(Transform playerPickup)
    {
        pickup = playerPickup;
        if (playerPickup.childCount > 0)
        {
            if (playerPickup.GetChild(0).CompareTag("Cheese"))
            {
                playerAnimator.SetTrigger("Interact");
                Invoke(nameof(Use), 0.9f);
                //Use(playerPickup);
                // cheeseShelf.FillShelf(objectPrefab);
                // foreach (Transform child in playerPickup)
                // {
                //     Destroy(child.gameObject);
                // }
            }
        }
    }

    private void Use()
    {
        cheeseShelf.FillShelf(objectPrefab);
        foreach (Transform child in pickup)
        {
            Destroy(child.gameObject);
        }
    }
}
