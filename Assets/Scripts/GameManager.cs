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
    private Vector3 pos;
    private AudioSource sonido;
    public AudioClip[] cortes;
    private int score = 0;
    public bool isGameActive;
    private int currentLives;
    private int contador = 0;
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
        pos = cursor.transform.position;
        sonido = GetComponent<AudioSource>();
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
    IEnumerator soundCountdownRoutine()
    {

        sonido.PlayOneShot(cortes[Random.Range(0, cortes.Length)], 1);
        yield return new WaitForSeconds(0.3f);
        pos = cursor.transform.position;
        contador = 0;
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
            if (Input.GetMouseButton(0))
            {
                if (contador == 0 && Mathf.Abs(cursor.transform.position.x - pos.x) > 0)
                {
                    contador++;
                    StartCoroutine(soundCountdownRoutine());

                }
            }


        }
        else
        {
            cursor.SetActive(false);
            Cursor.visible = true;
        }
    }
}
