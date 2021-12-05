using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DataModels;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public TMP_InputField UsernameInput;

    // Start is called before the first frame update
    void Start()
    {
        var gameData = DataManager.Instance.GameData;
        var username = gameData.RecentUsername;
        var highscore = gameData.HighScore;

        HighScoreText.text = $"Best Score: {highscore?.Username} - {highscore?.Score}";
        UsernameInput.interactable = true;
        UsernameInput.text = username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayClick()
    {
        DataManager.Instance.GameData.RecentUsername = UsernameInput.text;
        SceneManager.LoadScene(ProjectConstants.Scene.GameId);
    }

    public void OnExitClick()
    {
        DataManager.Instance.SaveData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
