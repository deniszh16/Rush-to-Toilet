using Data;
using Services.PersistentProgress;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "MyProgress";
        
        private readonly IPersistentProgressService _persistentProgress;
        
        public SaveLoadService(IPersistentProgressService progressService) =>
            _persistentProgress = progressService;
        
        public void SaveProgress() =>
            PlayerPrefs.SetString(ProgressKey, _persistentProgress.UserProgress.ToJson());

        public UserProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<UserProgress>();
    }
}