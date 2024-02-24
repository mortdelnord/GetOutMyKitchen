using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset playerInputs;
    private InputAction clickInput;
    public LayerMask ignoreRayCast;
    public GameObject clickParticles;
    public GameObject mouseClickParticles;
    private NavMeshAgent playerAgent;

    [Header ("Camera")]
    private Camera mainCam;

    [Header ("Bools")]
    
    //private bool canMove = true;
    private bool canInteract = true;
    private bool canClick = true;
    private Transform target;
    public Transform pickUpPoint;
    public Animator playerAnimator;
    public float interactTime;
    


    private GameObject currentCarriedObject;

    private void Start()
    {
        playerInputs.FindActionMap("Player").Enable();
        clickInput = playerInputs.FindActionMap("Player").FindAction("Click");
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        //Debug.Log(playerAgent.velocity.magnitude);
        if (playerAgent.velocity.magnitude > 0.1f)
        {
            //Debug.Log("ismoving");
            playerAnimator.SetBool("IsWalking", true);
            
        }else
        {
            //Debug.Log("isstopped");
            playerAnimator.SetBool("IsWalking", false);
        }
        if (pickUpPoint.childCount > 0)
        {
            playerAnimator.SetBool("IsCarrying", true);

        }else
        {
            playerAnimator.SetBool("IsCarrying", false);
        }


        if( clickInput.WasPressedThisFrame() && canClick)
        {
            //canClick = false;
            canInteract = true;
            Ray clickRay = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out RaycastHit hit, 1000, ~ignoreRayCast))
            {
                //Instantiate(clickParticles, hit.point, clickParticles.transform.rotation);
                if (hit.transform.gameObject.CompareTag("Mouse") || hit.transform.gameObject.CompareTag("CheeseMouse"))
                {
                    Instantiate(mouseClickParticles, hit.point, clickParticles.transform.rotation);
                    //canClick = false;
                    if (hit.transform.gameObject.CompareTag("Mouse"))
                    {
                        hit.transform.gameObject.GetComponent<MouseMovement>().isSearching = false;
                        hit.transform.gameObject.GetComponent<MouseMovement>().mouseAnimator.SetTrigger("isHit");
                        //canClick = true;

                    }else if(hit.transform.gameObject.CompareTag("CheeseMouse"))
                    {
                        BaseCheese cheeseScript = hit.transform.gameObject.GetComponent<BaseCheese>();
                        cheeseScript.ClearMice();
                        //canClick = true;
                        //cheeseScript.DetachMouse();
                    }
                }else
                {
                    if (hit.transform.gameObject.GetComponent<BaseSpawnPickUp>() != null)
                    {
                        Instantiate(clickParticles, hit.point, clickParticles.transform.rotation);
                        playerAgent.isStopped = false;
                        target = hit.transform;
                        playerAgent.SetDestination(target.position);
                        // Debug.Log(target);
                        // if (Vector3.Distance(transform.position, target.position) <= 4f && target != null && canInteract)
                        // {
                        //     canInteract = false;
                        //     Debug.Log("Is at destination");
                        //     playerAgent.isStopped = true;
                        //     playerAnimator.SetTrigger("Interact");
                        //     Invoke(nameof(Interact), interactTime);
                        //     // BaseSpawnPickUp objectScript = hit.transform.gameObject.GetComponent<BaseSpawnPickUp>();
                        //     // objectScript.Interact(pickUpPoint);
                        // }
                    }else
                    {
                        playerAgent.isStopped = false;
                        target = null;
                        playerAgent.SetDestination(hit.point);
                    }
                    //canClick = true;
                    //playerAgent.SetDestination(hit.point);
                    
                }

            }
            //Invoke(nameof(ResetClick), 0.2f);
            //canClick = true;
        }
        if (target != null)
        {

            if (Vector3.Distance(transform.position, target.position) <= 4f && target != null && canInteract)
            {
                if (target.gameObject.GetComponent<BaseSpawnPickUp>() != null)
                    {
                        canInteract = false;
                        //Debug.Log("Is at destination");
                        playerAgent.isStopped = true;
                        // playerAnimator.SetTrigger("Interact");
                        // Invoke(nameof(Interact), interactTime);
                        BaseSpawnPickUp objectScript = target.transform.gameObject.GetComponent<BaseSpawnPickUp>();
                        objectScript.Interact(pickUpPoint);
                        target = null;
                    }
            }
        }
    }

    private void ResetClick()
    {
        canClick = true;
    }
    // private void Interact()
    // {
        
    //     //Debug.Log("Is at destination");
    //     BaseSpawnPickUp objectScript = target.transform.gameObject.GetComponent<BaseSpawnPickUp>();
    //     objectScript.Interact(pickUpPoint);
        
    // }

    // private void ClearHands()
    // {
    //     if (pickUpPoint.childCount > 0)
    //     {
    //         foreach(Transform child in pickUpPoint)
    //         {
    //             Destroy(child.gameObject);
    //         }
    //     }
    // }

}
