using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject [] hazards;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private float t = 0.0f;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text powerupText;
    public AudioClip startMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioSource MusicSource;
    private int score;
    private bool gameOver;
    private bool restart;

    void Start ()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        powerupText.text= "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves ());
        MusicSource.clip=startMusic;
        MusicSource.Play();
    
    }
void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
            {
                Application.Quit();
            }
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.P))
            {
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown (KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {   
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'P' for restart";
                restart = true;
                break;
            
            }
        }
    }
public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }

    void UpdateScore()
    {
        scoreText.text = "Point: " + score;
        
        if (score >= 100)
        {
            winText.text = "You win!";
            gameOverText.text = "You win! Game Created by Cynthia";
            gameOver = true;
            restart = true;
            MusicSource.clip = winMusic;
            MusicSource.Play();
            var main = ps1.main;
                main.simulationSpeed = Mathf.Lerp(1 , 10 , 1);
                var secondary = ps2.main;
                secondary.simulationSpeed = Mathf.Lerp(1 , 10 , 2);
                t += 0.3f * Time.deltaTime;
        }
    }

    public void GameOver ()
    {
        gameOverText.text = "GAME OVER! GAME CREATED BY CYNTHIA";
        gameOver = true; 
        MusicSource.clip = loseMusic;
            MusicSource.Play();
    }
}