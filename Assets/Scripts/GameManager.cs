using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 2.0f;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifesText;
    public GameObject gameOverMenu;
    public GameObject titleScreen;
    public GameObject inGameScreen;
    public GameObject cursor;
    public GameObject trail;
    private int score = 0;
    public bool isGameActive;
    private int currentLives;
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Puntuacion: " + score;
    }
    public void updateLifes(int lifesToTake)
    {
        currentLives -= lifesToTake;
        lifesText.text = "Vidas: " + currentLives;
    }
    public void StartGame(int difficulty)
    {
        currentLives = 3;
        updateLifes(0);
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);
        inGameScreen.SetActive(true);
    }
    public void GameOver()
    {
        updateLifes(1);
        if (currentLives == 0)
        {
            gameOverMenu.SetActive(true);
            isGameActive = false;
        }
    }
    public void RestartGame()
    {
        inGameScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        if (isGameActive)
        {
            Cursor.visible = false;
            cursor.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                trail.SetActive(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                trail.SetActive(false);
            }
        }
        else
        {
            cursor.SetActive(false);
            Cursor.visible = true;
        }
    }
}
