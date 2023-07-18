﻿using Services.PersistentProgress;
using UnityEngine;
using Zenject;
using TMPro;

namespace Logic.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [Header("Количество очков")]
        [SerializeField] private TextMeshProUGUI _textComponent;

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        private void Awake() =>
            _progressService.UserProgress.ScoreChanged += UpdateScore;

        private void Start() =>
            UpdateScore();

        private void UpdateScore() =>
            _textComponent.text = _progressService.UserProgress.Score.ToString();

        private void OnDestroy() =>
            _progressService.UserProgress.ScoreChanged -= UpdateScore;
    }
}