using UnityEngine;
using TMPro;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    public Button menuButton;

    public GameObject wallPrefab;
    public GameObject spikePrefab;
    public GameObject spikesPrefab;
    public GameObject coinPrefab;

    int score = 0;
    int time = 0;
    bool isGameOver = false;
    bool isGamePaused = false;
    int nextMinSpawnTime = 1;

    private List<WallMovement> obstacles = new List<WallMovement>();

    void Start()
    {
        gameOverText.gameObject.SetActive(false);

        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        timeText.text = "Temps en vie: " + time;
        scoreText.text = "Score: " + score;
        StartCoroutine(UpdateTime());
        StartCoroutine(SpawnPrefabRandomly());
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            gameOverText.text = isGamePaused ? "Jeu en pause" : "";
            gameOverText.gameObject.SetActive(isGamePaused);
            if(isGamePaused)
            {
                StopAllCoroutines();
            } else
            {
                StartCoroutine(UpdateTime());
                StartCoroutine(SpawnPrefabRandomly());
            }

        }

        if (isGamePaused) return;

        MoveObstacles();
    }

    IEnumerator UpdateTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            time++;
            timeText.text = "Temps en vie: " + time;
            score += 10;
            scoreText.text = "Score: " + score;

            if (time % 5 == 0)
            {
                SpawnCoin();
            }
        }
    }

    IEnumerator SpawnPrefabRandomly()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(nextMinSpawnTime, 3));

            nextMinSpawnTime = PredictNextSpawn();
        }
    }

    void MoveObstacles()
    {
        for (int i = obstacles.Count - 1; i >= 0; i--)
        {
            WallMovement wall = obstacles[i];
            if (wall != null)
            {
                wall.transform.Translate(Vector3.left * 3 * Time.deltaTime, Space.World);

                if (wall.transform.position.x < -9)
                {
                    Destroy(wall.gameObject);
                    obstacles.RemoveAt(i);
                }
            }
        }
    }

    public void RegisterObstacles(WallMovement wall)
    {
        obstacles.Add(wall);
    }

    public void HandleCollision(GameObject player, Collision otherObject)
    {
        if (isGameOver) return;

        Debug.Log("Collision detected with " + otherObject.gameObject.tag);

        if (otherObject.gameObject.CompareTag("Trap"))
        {
            GameOver();
            Destroy(player);
        }
    }

    public void HandleTrigger(GameObject player, Collider otherObject)
    {
        if (otherObject.gameObject.CompareTag("Coin"))
        {
            score += 100;
            scoreText.text = "Score: " + score;
            Destroy(otherObject.gameObject);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.text = "Game Over !";
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        StopAllCoroutines();
        Debug.Log("Game Over !");
    }

    int PredictNextSpawn()
    {
        int random = UnityEngine.Random.Range(0, 3);
        switch (random)
        {
            case 0:
                Instantiate(wallPrefab, new Vector3(8, -1, -4), Quaternion.identity);
                return 1;
            case 1:
                Instantiate(spikePrefab, new Vector3(8, -1.26f, -4), Quaternion.identity);
                return 1;
            case 2:
                Instantiate(spikesPrefab, new Vector3(9, -1.26f, -1.2f), Quaternion.identity);
                return 2;
            default:
                return 1;
        }
    }

    void SpawnCoin()
    {
        Instantiate(coinPrefab, new Vector3(8, -0.3f, -4.1f), Quaternion.identity);
    }
}
