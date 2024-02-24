using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketPileScript : BaseSpawnPickUp
{

    public Animator playerAnimator;
    Transform pickup;
    public AudioSource interactSound;
    public override void Interact(Transform playerPickup)
    {
        pickup = playerPickup;
        if(playerPickup.childCount > 0)
        {
            return;
        }else
        {
            playerAnimator.SetTrigger("Interact");
            interactSound.Play();
            Invoke(nameof(Use), 0.9f);
            //Instantiate(objectPrefab, playerPickup);
        }
    }



    private void Use()
    {
        Instantiate(objectPrefab, pickup);
    }
}
