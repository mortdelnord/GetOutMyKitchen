using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    public bool AddMouse(GameObject mouse)
    {
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
        
        //cheeseAgent = mouseAgent;
        cheeseAgent.SetDestination(gameManager.RandomMouseHole().position);

    }

    public void ClearMice()
    {
        foreach(GameObject mouse in mice)
        {
            mice.Remove(mouse);
            mouse.GetComponent<MouseMovement>().canMove = true;
            mouse.GetComponent<MouseMovement>().isSearching = false;
        }
    }

    public void AttachMouse(Transform mouseBody)
    {
        int mouseInt = mice.IndexOf(newestMouse);
        Transform mousePoint = carryPoints[mouseInt];
        mouseBody.parent = mousePoint;
        mouseBody.localPosition = Vector3.zero;
        if (mice.Count == miceCountMax)
        {
            MoveCheese(mouseBody.gameObject.GetComponent<NavMeshAgent>());
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
                mouseScript.mouseAgent.enabled = true;
                mouseScript.canMove = true;

            }
        }
    }





}
