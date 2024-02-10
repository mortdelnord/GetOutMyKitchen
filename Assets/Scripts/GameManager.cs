using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> cheeseList;

    public List<Transform> mouseHoles;

    public int score = 0;

    public Transform RandomMouseHole()
    {
        int randInt = Random.Range(0, mouseHoles.Count - 1);
        Transform point = mouseHoles[randInt].transform;
        return point;
    }
    public GameObject RandomCheese()
    {
        int randInt = Random.Range(0, cheeseList.Count -1);
        GameObject cheese = cheeseList[randInt];
        return cheese;
    }



    public void UpdateScore()
    {
        score = 0;
        foreach(GameObject cheese in cheeseList)
        {
            score += 1;
        }
    }


}
