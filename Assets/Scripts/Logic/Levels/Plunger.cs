using UnityEngine;

namespace Logic.Levels
{
    public class Plunger : MonoBehaviour
    {
        [Header("Цвет объекта")]
        [SerializeField] private ObjectColor _objectColor;

        public ObjectColor ObjectColor => _objectColor;

        [Header("Препятствие")]
        [SerializeField] private Obstruction _obstruction;

        [Header("Анимация вантуза")]
        [SerializeField] private Animator _animator;
        
        private static readonly int ActionAnimation = Animator.StringToHash("Action");

        public void ActivatePlunger()
        {
            _animator.SetTrigger(ActionAnimation);
            _obstruction.RemoveObstruction();
        }
    }
}