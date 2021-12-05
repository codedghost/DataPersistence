
using System;
using System.Collections.Generic;
using Assets.Scripts.DataModels;

namespace Assets.Scripts.DataModels
{
    [Serializable]
    public class GameData
    {
        public string RecentUsername;
        public List<HighScore> HighScores;
        public HighScore HighScore;
    }
}