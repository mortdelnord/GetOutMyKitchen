using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMovement : MonoBehaviour
{
    public GameManager gameManager;
    public NavMeshAgent mouseAgent;
    public float range = 10.0f;
    public float timerMax = 5.0f;
    private float timer = 0f;
    public bool canMove = true;
    public bool isCarrying = false;

    public bool isSearching = true;
    public Transform target = null;
    
    public Transform home;


    private void Start()
    {
        //Debug.Log(target);
        
    }
    private void Awake()
    {
        mouseAgent = gameObject.GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        target = gameManager.RandomCheese().transform;
        timer = timerMax;
        //Debug.Log(target);
        home = gameManager.RandomMouseHole();
        //Debug.Log(home);

    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result) // gets a randoim point on the navmesh
    {
        for (int i = 0; i < 30; i++)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.GetAreaFromName("PreyArea")))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void Update()
    {
        if (canMove)
        {
            if (isSearching)
            {
                mouseAgent.SetDestination(target.position);
                if (!target.gameObject.CompareTag("Shelf"))
                {

                    if (target.gameObject.GetComponent<BaseCheese>().isFull && target != null)
                    {
                        //isSearching = false;
                        if(gameManager.cheeseList.Count > 0)
                        {

                            target = gameManager.RandomCheese().transform;
                        }else
                        {
                            isSearching = false;
                        }
                    }
                }else 
                {
                    CheeseShelf shelfScript = target.gameObject.GetComponent<CheeseShelf>();
                    if (target.gameObject.GetComponent<CheeseShelf>().shelfList.Count > 0)
                    {

                        if (gameManager.cheeseList.Count > 0)
                        {
                            target = gameManager.RandomCheese().transform;
                        }else
                        {
                            shelfScript.RemoveFromCheeseList();
                            isSearching = false;
                        }
                    }else
                    {
                        isSearching = false;
                        shelfScript.RemoveFromCheeseList();
                    }
                }
                if(Vector3.Distance(transform.position, target.position) <= 1f && target != null)
                {
                    isSearching = false;
                    canMove = false;
                    if (target.gameObject.CompareTag("Shelf"))
                    {
                        CheeseShelf shelfScript = target.gameObject.GetComponent<CheeseShelf>();
                        if(shelfScript.shelfList.Count > 0)
                        {
                            PickUpCheese(shelfScript.RemoveFromShelf());

                        }else 
                        {
                            shelfScript.RemoveFromCheeseList();
                            canMove = true;
                        }
                    }else
                    {
                        PickUpCheese(target.gameObject);

                    }

                }
            }else
            {
                mouseAgent.SetDestination(home.position);
                
                    if (Vector3.Distance(transform.position, home.position) <= 1f)
                    {

                        Destroy(gameObject);
                        //PickUpCheese(target.gameObject);
                    }
                
            }
            
        }else
        {
            mouseAgent.enabled = false;
        }
    }

    private void PickUpCheese(GameObject cheese)
    {
        BaseCheese cheeseScript = cheese.GetComponent<BaseCheese>();
        if (cheeseScript.AddMouse(gameObject))
        {
            mouseAgent.enabled = false;
            cheeseScript.AttachMouse(transform);
        }else
        {
            mouseAgent.enabled = true;
            target = gameManager.RandomCheese().transform;
            isSearching = true;
            canMove = true;

        }
    }


}
