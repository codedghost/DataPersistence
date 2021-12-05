using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DataModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreTextPanel;
    public int TotalHighscoresToDisplay = 10;

    // Start is called before the first frame update
    void Start()
    {
        var highScores = DataManager.Instance.GameData.HighScores.OrderByDescending(h => h.Score).ToList();
        var leftOvers = TotalHighscoresToDisplay - highScores.Count();

        List<string> highScoreList = new List<string>();
        for (var i = 0; i < highScores.Count; i++)
        {
            highScoreList.Add(FormatHighScoreText(highScores[i], i));
        }

        for (var i = highScores.Count; i < TotalHighscoresToDisplay; i++)
        {
            highScoreList.Add(FormatHighScoreText(new HighScore { Username = "AAA", Score = 0 }, i));
        }

        HighScoreTextPanel.text = string.Join("\n", highScoreList);
    }

    private string FormatHighScoreText(HighScore highScore, int index)
    {
        return $"{GetFormattedPositionString(index+1)}. {highScore.Username} - {highScore.Score}";
    }

    private string GetFormattedPositionString(int index)
    {
        // account for higher values
        var remainder = index % 100;
        switch (remainder)
        {
            case 1:
                return $"{index}st";
            case 2:
                return $"{index}nd";
            case 3:
                return $"{index}rd";
            default:
                return $"{index}th";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(ProjectConstants.Scene.MenuId);
        } 
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(ProjectConstants.Scene.GameId);
        }
    }
}
