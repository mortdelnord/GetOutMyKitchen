using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseCheese : MonoBehaviour
{
    public GameManager gameManager;
    public int miceCountMax;
    public List<GameObject> mice;
    public List<Transform> carryPoints;

    private GameObject newestMouse;

    private Transform home;
    public Rigidbody cheeseRb;
    public bool isFull = false;
    private bool isGoingHome = false;

    private void Start()
    {
        //cheeseRb = gameObject.GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        if (transform.parent == null)
        {
            gameManager.cheeseList.Add(gameObject);
        }
        //cheeseRb = gameObject.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(isGoingHome)
        {
            if (Vector3.Distance(transform.position, home.position) <= 1f)
            {
                //Debug.Log("Is Destroying");
                Destroy(gameObject);
                //PickUpCheese(target.gameObject);
            }
           
        }
    }
    public bool AddMouse(GameObject mouse)
    {
        cheeseRb.isKinematic = true;
        transform.tag = "CheeseMouse";
        if (gameObject.GetComponent<BoxCollider>() == null)
        {
            BoxCollider cheeseTrigger = gameObject.AddComponent<BoxCollider>();
            cheeseTrigger.isTrigger = true;
        }
        if (mice.Count < miceCountMax)
        {

            mice.Add(mouse);
            newestMouse = mouse;
            
            return true;
        }else
        {
            //MoveCheese(mouse.GetComponent<NavMeshAgent>());
            return false;
        }
    }

    public void MoveCheese(NavMeshAgent mouseAgent)
    {
        NavMeshAgent cheeseAgent = gameObject.AddComponent<NavMeshAgent>();
        cheeseAgent.baseOffset = 0.5f;
        
        //cheeseAgent = mouseAgent;
        home = gameManager.RandomMouseHole();
        cheeseAgent.SetDestination(home.position);
        isGoingHome = true;
    }

    public void ClearMice()
    {
        mice.Clear();
        DetachMouse();

    }

    public void AttachMouse(Transform mouseBody)
    {
        int mouseInt = mice.IndexOf(newestMouse);
        Transform mousePoint = carryPoints[mouseInt];
        mouseBody.parent = mousePoint;
        mouseBody.localPosition = Vector3.zero;
        mouseBody.rotation = transform.rotation;
        if (mice.Count == miceCountMax)
        {
            gameManager.cheeseList.Remove(gameObject);
            isFull = true;
            MoveCheese(mouseBody.gameObject.GetComponent<NavMeshAgent>());
        }else 
        {
            Transform newPoint = gameManager.RandomMouseHole();
            GameObject neededMouse = Instantiate(gameManager.mousePrefab, newPoint.position, mousePoint.rotation);
            neededMouse.GetComponent<MouseMovement>().target = gameObject.transform;
        }
    }
    public void DetachMouse()
    {
        foreach (Transform point in carryPoints)
        {
            if (point.childCount > 0)
            {
                Transform mouseTransform = point.GetChild(0);
                mouseTransform.parent = null;
                MouseMovement mouseScript = mouseTransform.gameObject.GetComponent<MouseMovement>();
                mouseScript.mouseAnimator.SetTrigger("isHit");
                mouseScript.mouseAgent.enabled = true;
                mouseScript.canMove = true;
                mouseScript.isSearching = false;
                isFull = false;
            }
        }
        if (!gameManager.cheeseList.Contains(gameObject))
        {
            gameManager.cheeseList.Add(gameObject);
            gameManager.UpdateScore();
        }
        if (gameObject.GetComponent<NavMeshAgent>() != null)
        {

            Destroy(gameObject.GetComponent<NavMeshAgent>());
        }
        Invoke(nameof(ChangeTagToCheese), 0.2f);
        cheeseRb.isKinematic = false;
    }

    private void ChangeTagToCheese()
    {
        transform.tag = "Cheese";
    }



}
