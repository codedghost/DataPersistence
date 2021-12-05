using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.DataModels;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public GameData GameData = new GameData();

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(GameData);
        File.WriteAllText(ProjectConstants.GameDataDirectoryAndFileName, json);
    }

    public void LoadData()
    {
        if (File.Exists(ProjectConstants.GameDataDirectoryAndFileName))
        {
            var json = File.ReadAllText(ProjectConstants.GameDataDirectoryAndFileName);

            this.GameData = JsonUtility.FromJson<GameData>(json);

            if (GameData.RecentUsername == null) GameData.RecentUsername = string.Empty;
            if (GameData.HighScores == null) GameData.HighScores = new List<HighScore>();
        }
    }
}
