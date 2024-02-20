using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> cheeseList;

    public List<Transform> mouseHoles;
    public GameObject mousePrefab;
    public CheeseShelf cheeseShelf;

    private bool canSpawn = true;

    public int score = 0;
    private float spawnTimer = 0f;
    public float spawnTimerMax;
    public float intialTime;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        canSpawn = false;
    }
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
        int previousScore = score;
        score = 0;
        foreach(GameObject cheese in cheeseList)
        {
            if (cheese.GetComponent<CheeseShelf>() != null)
            {
                int shelfCount = cheese.GetComponent<CheeseShelf>().shelfList.Count;
                score += shelfCount;
            }else
            {
                score += 1;
            }
        }

        Difficulty(previousScore);
        scoreText.text = score.ToString();
    }

    private void Update()
        {
            if (score != (cheeseList.Count + (cheeseShelf.shelfList.Count - 1)))
            {
                UpdateScore();

            }
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


        private void Difficulty(int previousScore)
        {
            if (score != 0)
            {
                canSpawn = true;
                if (previousScore == 0)
                {
                    spawnTimerMax = intialTime;
                }else
                {

                    if (score % 5 == 0)
                    {
                        if (previousScore < score)
                        {
                            spawnTimerMax -= 1f;
                        }else
                        {
                            spawnTimerMax += 1f;
                        }
                    }
                }

            }else
            {
                canSpawn = false;
            }
        }
    
}
