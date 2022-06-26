using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int startingLives = 5;
    [Range(1, 1000)]
    [SerializeField] int pointsPerLevel = 500;
    [SerializeField] ObjectSpawner[] spawners;

    [Header("% Adjustments Per level")]
    [Range(0,100)]
    [SerializeField] float objectSpeedIncrease = 3.0f;
    [Range(0, 100)]
    [SerializeField] float fireRateIncrease = 3.0f;

    [Header("Heads Up Display")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;

    private int lives;
    private int score = 0;
    private int level = 1;
    private int levelPoints; // number of points since the last level

    public bool GameRunning { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        lives = startingLives;

        if (lives > 0)
        {
            gameOverText.gameObject.SetActive(false);
            GameRunning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update GUI text
        levelText.text = level.ToString();
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();
    }
    
    public void AddPoints(int amount)
    {
        score += amount;
        levelPoints += amount;

        if(levelPoints >= pointsPerLevel)
            IncreaseLevel();
    }
    public void IncreaseLevel()
    {
        level++;
        levelPoints = score % pointsPerLevel;

        foreach (ObjectSpawner spawner in spawners)
        {
            spawner.IncreaseTravelSpeed(objectSpeedIncrease);
            spawner.ReduceTimeDelay(fireRateIncrease);
        }
    }

    public void LoseLife()
    {
        lives--;

        if (lives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            GameRunning = false;
            Time.timeScale = 0;
        }
    }

}
