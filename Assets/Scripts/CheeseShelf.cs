using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseShelf : MonoBehaviour
{
    private GameManager gameManager;
    public List<GameObject> shelfList;
    public List<Transform> shelfVisualPoints;
    public GameObject cheeseVisual;
    public Transform dropPoint;
    public int shelfMax;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (shelfList.Count > 0)
        {
            AddToCheeseList();
        }

    }

    private void AddToCheeseList()
    {
        if (!gameManager.cheeseList.Contains(gameObject))
        {
            gameManager.cheeseList.Add(gameObject);
        }
    }
    public void RemoveFromCheeseList()
    {
        if (gameManager.cheeseList.Contains(gameObject))
        {
            gameManager.cheeseList.Remove(gameObject);
        }
    }
    public void FillShelf(GameObject cheese)
    {
        if (shelfList.Count < shelfMax)
        {
            
            shelfList.Add(cheese);
            int cheeseInt = shelfList.Count - 1;
            //Debug.Log(cheeseInt);
            if (cheeseInt <= shelfVisualPoints.Count - 1)
            {
                Transform shelfPoint = shelfVisualPoints[cheeseInt];
                Instantiate(cheeseVisual, shelfPoint.position, RandomCheesRotation(), shelfPoint);
            }

        }else if (shelfList.Count >= shelfMax)
        {
            GameObject droppedCheese = Instantiate(cheese, dropPoint.position, dropPoint.rotation);
            droppedCheese.GetComponent<Rigidbody>().isKinematic = false;
        }
        AddToCheeseList();
    }
    public GameObject RemoveFromShelf()
    {
        if (shelfList.Count > 0)
        {
            int removedInt = shelfList.Count - 1;
            GameObject removedCheese = shelfList[removedInt];
            if (shelfList.Count - 1 <= shelfVisualPoints.Count - 1)
            {
                Transform removedVisual = shelfVisualPoints[removedInt];
                if (removedVisual.childCount > 0)
                {  
                    foreach( Transform child in removedVisual)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            shelfList.Remove(removedCheese);
            removedCheese = Instantiate(removedCheese, dropPoint.position, dropPoint.rotation);
            //Debug.Log("cheese removed and spawned");
            removedCheese.GetComponent<Rigidbody>().isKinematic = false;
            return removedCheese;

        }else
        {
            RemoveFromCheeseList();
            return null;
        }
        // BaseCheese cheese = removedCheese.GetComponent<BaseCheese>();
        // if (cheese.AddMouse(mouse))
        // {
        //     cheese.AttachMouse(mouse.transform);
        // }
    }


    private Quaternion RandomCheesRotation()
    {
        float yangle = Random.Range(-270, 270);
        Quaternion newAngle = Quaternion.Euler(0f, yangle, 0f);
        return newAngle;
    }
}
