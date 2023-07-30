using UnityEngine;

namespace Logic.Levels
{
    public abstract class Animal : MonoBehaviour
    {
        [Header("Скорость движения")]
        [SerializeField] protected float _speed;

        [Header("Необходимые компоненты")]
        [SerializeField] protected Transform _transform;
        [SerializeField] protected Transform _childObjectTransform;
        [SerializeField] protected Animator _animator;
        
        protected static readonly int IdleAnimation = Animator.StringToHash("Idle");
        protected static readonly int AttackAnimation = Animator.StringToHash("Attack");
        protected static readonly int WalkAnimation = Animator.StringToHash("Walk");
        protected static readonly int DeadAnimation = Animator.StringToHash("Dead");

        protected bool _movement;
        protected Vector3 _moveToLeft;

        protected virtual void Start() =>
            _moveToLeft = new(0f, 180f, 0f);

        protected void SetAnimation(int animationClip) =>
            _animator.SetTrigger(animationClip);

        public void AttackCharacter(Transform character)
        {
            StopAllCoroutines();
            RotateObject(character.transform.position);
            SetAnimation(AttackAnimation);
            _movement = false;
        }

        protected void RotateObject(Vector3 point)
        {
            _childObjectTransform.rotation = _transform.position.x > point.x
                ? Quaternion.Euler(Vector3.zero)
                : Quaternion.Euler(_moveToLeft);
        }
    }
}