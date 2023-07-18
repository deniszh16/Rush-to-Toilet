using UnityEngine;

namespace Logic.Levels
{
    public class Toilet : MonoBehaviour
    {
        [Header("Цвет объекта")]
        [SerializeField] private ObjectColor _objectColor;
        
        [Header("Анимация туалета")]
        [SerializeField] private Animator _animator;
        
        private static readonly int PrepAnimation = Animator.StringToHash("Preparation");
        
        [Header("Эффект брызг")]
        [SerializeField] private ParticleSystem _spray;

        private Character _attachedCharacter;

        public ObjectColor ObjectColor => _objectColor;

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
        }

        private void OnDestroy() =>
            _attachedCharacter.Dived -= ShowToiletAnimation;
    }
}