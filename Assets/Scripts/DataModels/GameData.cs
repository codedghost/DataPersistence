
using System;
using System.Collections.Generic;
using Assets.Scripts.DataModels;

namespace Assets.Scripts.DataModels
{
    [Serializable]
    public class GameData
    {
        public string RecentUsername { get; set; }
        public List<HighScore> HighScores { get; set; }
    }
}