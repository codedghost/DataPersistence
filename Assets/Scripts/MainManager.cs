using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DataModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        UpdateHighscoreText();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(ProjectConstants.Scene.HighscoreId);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(ProjectConstants.Scene.MenuId);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        var gameData = DataManager.Instance.GameData;
        if (gameData.HighScore == null || gameData.HighScore.Score < m_Points)
        {
            DataManager.Instance.GameData.HighScore = new HighScore
            {
                Username = gameData.RecentUsername,
                Score = m_Points
            };

            UpdateHighscoreText();
        }
    }

    void UpdateHighscoreText()
    {
        var highscore = DataManager.Instance.GameData.HighScore;

        HighScoreText.text = $"Best Score: {highscore?.Username}: {highscore?.Score}";
    }

    public void GameOver()
    {
        DataManager.Instance.GameData.HighScores.Add(new HighScore
        {
            Username = DataManager.Instance.GameData.RecentUsername,
            Score = m_Points
        });

        DataManager.Instance.SaveData();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
