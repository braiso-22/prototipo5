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
    public GameObject gameOverMenu;
    public GameObject titleScreen;
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
    public void StartGame(int difficulty)
    {
        currentLives = 3;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        if (currentLives == 1)
        {
            gameOverMenu.SetActive(true);
            isGameActive = false;
        }
        else
        {
            currentLives--;
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
