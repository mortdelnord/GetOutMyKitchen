using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> cheeseList;

    public List<Transform> mouseHoles;
    public GameObject mousePrefab;

    private bool canSpawn = true;

    public int score = 0;
    private float spawnTimer = 0f;
    public float spawnTimerMax;


    public Transform RandomMouseHole()
    {
        int randInt = Random.Range(0, mouseHoles.Count - 1);
        Transform point = mouseHoles[randInt].transform;
        return point;
    }
    public GameObject RandomCheese()
    {
        if (cheeseList.Count > 0)
        {
            int randInt = Random.Range(0, cheeseList.Count - 1);
            GameObject cheese = cheeseList[randInt];
            if (cheese == null)
            {
                randInt = Random.Range(0, cheeseList.Count);
                cheese = cheeseList[randInt];
            }
            return cheese;

        }else
        {
            return null;
        }
    }



    public void UpdateScore()
    {
        score = 0;
        foreach(GameObject cheese in cheeseList)
        {
            score += 1;
        }
    }

    private void Update()
        {
            spawnTimer += Time.deltaTime;
            if (canSpawn && spawnTimer >= spawnTimerMax)
            {
                if (cheeseList.Count > 0)
                {

                    spawnTimer = 0f;

                    Transform spawnPoint = RandomMouseHole();
                    //Vector3 spawnPos = new Vector3(spawnPoint.position.x, 0f, spawnPoint.position.z);
                    Instantiate(mousePrefab, spawnPoint.position, spawnPoint.rotation);
                }
            }
        }
    
}
