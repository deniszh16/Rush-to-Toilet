using System;
using Logic.Sounds;
using UnityEngine;

namespace Logic.Levels
{
    public class Toilet : MonoBehaviour
    {
        [Header("Цвет объекта")]
        [SerializeField] private ObjectColor _objectColor;
        
        [Header("Анимация туалета")]
        [SerializeField] private Animator _animator;
        
        [Header("Звук брызг")]
        [SerializeField] private PlayingSound _playingSound;
        
        private static readonly int PrepAnimation = Animator.StringToHash("Preparation");
        
        [Header("Эффект брызг")]
        [SerializeField] private ParticleSystem _spray;

        private Character _attachedCharacter;

        public ObjectColor ObjectColor => _objectColor;

        public event Action ToiletIsFull;

        public void SetAttachedCharacter(Character character)
        {
            _attachedCharacter = character;
            _attachedCharacter.Dived += ShowToiletAnimation;
        }

        private void ShowToiletAnimation()
        {
            _animator.SetTrigger(id: PrepAnimation);
            _spray.gameObject.SetActive(true);
            _spray.Play();
            _playingSound.PlaySound();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Character character))
            {
                if (ObjectColor == character.ObjectColor)
                {
                    character.DisableCollider();
                    ToiletIsFull?.Invoke();
                }
            }
        }

        private void OnDestroy()
        {
            if (_attachedCharacter != null)
                _attachedCharacter.Dived -= ShowToiletAnimation;
        }
    }
}