using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent playerAgent;

    [Header ("Camera")]
    private Camera mainCam;

    [Header ("Bools")]
    
    private bool canMove = true;
    private bool canInteract = false;
    private bool canClick = true;
    private Transform target;
    public Transform pickUpPoint;


    private GameObject currentCarriedObject;

    private void Start()
    {
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if( Input.GetMouseButton(0) && canClick)
        {
            canInteract = true;
            Ray clickRay = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out RaycastHit hit, 1000, playerAgent.areaMask))
            {
                if (hit.transform.gameObject.CompareTag("Mouse") || hit.transform.gameObject.CompareTag("CheeseMouse"))
                {
                    //canClick = false;
                    if (hit.transform.gameObject.CompareTag("Mouse"))
                    {
                        hit.transform.gameObject.GetComponent<MouseMovement>().isSearching = false;
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
                        target = hit.transform;
                        playerAgent.SetDestination(target.position);
                        Debug.Log(target);
                        if (Vector3.Distance(transform.position, target.position) <= 5f && target != null)
                        {
                            Debug.Log("Is at destination");
                            BaseSpawnPickUp objectScript = hit.transform.gameObject.GetComponent<BaseSpawnPickUp>();
                            objectScript.Interact(pickUpPoint);
                        }
                    }else
                    {
                        target = null;
                        playerAgent.SetDestination(hit.point);
                    }
                    //canClick = true;
                    //playerAgent.SetDestination(hit.point);
                    
                }

            }
        }
        if (target != null)
        {

            if (Vector3.Distance(transform.position, target.position) <= 2f && target != null && canInteract)
            {
                canInteract = false;
                //Debug.Log("Is at destination");
                BaseSpawnPickUp objectScript = target.transform.gameObject.GetComponent<BaseSpawnPickUp>();
                objectScript.Interact(pickUpPoint);
            }
        }
    }


    private void ClearHands()
    {
        if (pickUpPoint.childCount > 0)
        {
            foreach(Transform child in pickUpPoint)
            {
                Destroy(child.gameObject);
            }
        }
    }

}
