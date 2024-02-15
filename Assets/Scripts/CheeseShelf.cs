using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseShelf : MonoBehaviour
{
    private GameManager gameManager;
    public List<GameObject> shelfList;
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
            shelfList.Remove(removedCheese);
            removedCheese = Instantiate(removedCheese, dropPoint.position, dropPoint.rotation);
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
}
