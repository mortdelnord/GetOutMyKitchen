using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    private GameObject currentCarriedObject;

    private void Start()
    {
        playerAgent = gameObject.GetComponent<NavMeshAgent>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        if( Input.GetMouseButton(0))
        {
            Ray clickRay = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out RaycastHit hit, 1000, playerAgent.areaMask))
            {
                if (hit.transform.gameObject.CompareTag("Mouse"))
                {
                    hit.transform.gameObject.GetComponent<MouseMovement>().isSearching = false;
                }else
                {

                    playerAgent.SetDestination(hit.point);
                }

            }
        }
    }
}
