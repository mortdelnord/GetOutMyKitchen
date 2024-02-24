using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class CowScript : BaseSpawnPickUp
{
    public Animator playerAnimator;
    public ParticleSystem milkParticles;
    public AudioSource milkSound;

    Transform pickup;
    public override void Interact(Transform playerPickup)
    {
        pickup = playerPickup;
        if (playerPickup.childCount > 0)
        {
            if (playerPickup.GetChild(0).CompareTag("Bucket"))
            {
                playerAnimator.SetTrigger("Interact");
                Invoke(nameof(Use), 0.9f);

                // foreach(Transform child in playerPickup)
                // {
                //     Destroy(child.gameObject);
                // }
                // Instantiate(objectPrefab, playerPickup);
            }

        }
    }



    private void Use()
    {
        milkSound.Play();
        milkParticles.Play();
        //cheeseShelf.FillShelf(objectPrefab);
        foreach (Transform child in pickup)
        {
            Destroy(child.gameObject);
        }
        Instantiate(objectPrefab, pickup);
    }
}
