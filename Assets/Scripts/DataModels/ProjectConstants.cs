using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public class ProjectConstants
    {
        public class Scene
        {
            public const int MenuId = 0;
            public const int GameId = 1;
            public const int HighscoreId = 2;
        }

        public const string GameDataFileName = "savefile.json";
        public static string GameDataDirectory = Application.persistentDataPath;
        public static string GameDataDirectoryAndFileName = $"{GameDataDirectory}/{GameDataFileName}";
    }
}