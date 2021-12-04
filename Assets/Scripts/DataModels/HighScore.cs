
using System;

namespace Assets.Scripts.DataModels
{
    [Serializable]
    public class HighScore
    {
        public string Username { get; set; }
        public int Score { get; set; }
    }
}