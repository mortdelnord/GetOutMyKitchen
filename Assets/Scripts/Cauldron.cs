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
    Transform pickup;
    public ParticleSystem cookingParticles;
    public ParticleSystem sparkles;
    public ParticleSystem smoke;
    public AudioSource bubbleSound;
    public AudioSource finishedSound;
    public AudioSource popSound;
    


    public Animator playerAnimator;
    public override void Interact(Transform playerPickup)
    {
        pickup = playerPickup;
        if(playerPickup.childCount > 0)
        {
            if (playerPickup.GetChild(0).CompareTag("Milk") && !isCooking)
            {
                playerAnimator.SetTrigger("Interact");
                Invoke(nameof(Use), 0.9f);
                // foreach (Transform child in playerPickup)
                // {
                //     Destroy(child.gameObject);
                // }
                // isCooking = true;
                //Cooking();
            }
        }else
        {
            if (doneCooking)
            {
                playerAnimator.SetTrigger("Interact");
                Invoke(nameof(CookingDone), 0.9f);
            }
            // if (doneCooking)
            // {
            //     Instantiate(objectPrefab, playerPickup);
            //     doneCooking = false;
            // }
        }
    }

    private void Update()
    {
        if (isCooking)
        {
            //bubbleSound.Play();
            cookTimer += Time.deltaTime;
            if (cookTimer >= cookTimerMax)
            {
                bubbleSound.Stop();
                finishedSound.Play();
                cookingParticles.Stop();
                sparkles.Play();
                smoke.Play();
                cookTimer = 0f;
                //Debug.Log("Done Cooking");
                isCooking = false;
                doneCooking = true;
            }
        }

    }




    private void Use()
    {
        
        foreach (Transform child in pickup)
        {
            Destroy(child.gameObject);
        }
        isCooking = true;
        cookingParticles.Play();
        bubbleSound.Play();

        
        // if (doneCooking)
        // {
        //     Instantiate(objectPrefab, pickup);
        //     doneCooking = false;
        // }
        
    }
    private void CookingDone()
    {
        finishedSound.Stop();
        popSound.Play();
        Instantiate(objectPrefab, pickup);
        doneCooking = false;
        
    }


    
}
