using System.Collections;
using System.Collections.Generic;
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
            
            canInteract = true;
            Ray clickRay = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out RaycastHit hit, 1000, ~ignoreRayCast))
            {
                
                if (hit.transform.gameObject.CompareTag("Mouse") || hit.transform.gameObject.CompareTag("CheeseMouse"))
                {
                    Instantiate(mouseClickParticles, hit.point, clickParticles.transform.rotation);
                    
                    if (hit.transform.gameObject.CompareTag("Mouse"))
                    {
                        hit.transform.gameObject.GetComponent<MouseMovement>().isSearching = false;
                        hit.transform.gameObject.GetComponent<MouseMovement>().mouseAnimator.SetTrigger("isHit");
                        

                    }else if(hit.transform.gameObject.CompareTag("CheeseMouse"))
                    {
                        BaseCheese cheeseScript = hit.transform.gameObject.GetComponent<BaseCheese>();
                        cheeseScript.ClearMice();
                        
                    }
                }else
                {
                    if (hit.transform.gameObject.GetComponent<BaseSpawnPickUp>() != null)
                    {
                        Instantiate(clickParticles, hit.point, clickParticles.transform.rotation);
                        playerAgent.isStopped = false;
                        target = hit.transform;
                        playerAgent.SetDestination(target.position);
                        
                    }else
                    {
                        playerAgent.isStopped = false;
                        target = null;
                        playerAgent.SetDestination(hit.point);
                    }
                    
                    
                }

            }
            
        }
        if (target != null)
        {

            if (Vector3.Distance(transform.position, target.position) <= 4f && target != null && canInteract)
            {
                if (target.gameObject.GetComponent<BaseSpawnPickUp>() != null)
                    {
                        canInteract = false;
                        playerAgent.isStopped = true; 
                        BaseSpawnPickUp objectScript = target.transform.gameObject.GetComponent<BaseSpawnPickUp>();
                        objectScript.Interact(pickUpPoint);
                        target = null;
                    }
            }
        }
    }

   

}
