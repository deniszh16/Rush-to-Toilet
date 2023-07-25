using Services.Achievements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    public class Achievement : MonoBehaviour
    {
        [Header("Номер достижения")]
        [SerializeField] private int _number;
        
        [Header("Компоненты карточки")]
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _outline;
        
        private IAchievementsService _achievementsService;

        [Inject]
        private void Construct(IAchievementsService achievementsService) =>
            _achievementsService = achievementsService;

        private void Start()
        {
            if (_achievementsService.CheckAchievement(_number))
            {
                _icon.color = Color.white;
                _outline.SetActive(true);
            }
        }
    }
}