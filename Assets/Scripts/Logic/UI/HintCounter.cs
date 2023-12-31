﻿using Services.PersistentProgress;
using UnityEngine;
using Zenject;
using TMPro;

namespace Logic.UI
{
    public class HintCounter : MonoBehaviour
    {
        [Header("Текстовый компонент")]
        [SerializeField] private TextMeshProUGUI _textComponent;
        
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;
        
        private void Awake() =>
            _progressService.GetUserProgress.HintsChanged += UpdateNumberOfHints;

        private void Start() =>
            UpdateNumberOfHints();

        private void UpdateNumberOfHints() =>
            _textComponent.text = _progressService.GetUserProgress.Hints.ToString();

        private void OnDestroy() =>
            _progressService.GetUserProgress.HintsChanged -= UpdateNumberOfHints;
    }
}