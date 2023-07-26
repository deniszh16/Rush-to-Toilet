using UnityEngine;

namespace Logic.Levels
{
    public class Obstruction : Obstacle
    {
        [Header("Анимация препятствия")]
        [SerializeField] private Animator _animator;
        
        private static readonly int ActionAnimation = Animator.StringToHash("Action");
        
        public void RemoveObstruction() =>
            _animator.SetTrigger(ActionAnimation);
    }
}