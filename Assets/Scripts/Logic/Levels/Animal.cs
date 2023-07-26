using System.Collections;
using UnityEngine;

namespace Logic.Levels
{
    public class Animal : MonoBehaviour
    {
        [Header("Точки для перемещения")]
        [SerializeField] private Transform[] _points;

        [Header("Скорость движения")]
        [SerializeField] private float _speed;
        
        [Header("Пауза при повороте")]
        [SerializeField] private float _pause;
        
        [Header("Необходимые компоненты")]
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _childObjectTransform;
        [SerializeField] private Animator _animator;
        
        private static readonly int IdleAnimation = Animator.StringToHash("Idle");
        private static readonly int AttackAnimation = Animator.StringToHash("Attack");
        private static readonly int WalkAnimation = Animator.StringToHash("Walk");

        private bool _movement;
        private int _currentPoint;
        private Vector3 _moveToLeft;

        private void Start()
        {
            _movement = true;
            _currentPoint = 0;
            _moveToLeft = new(0f, 180f, 0f);
            SetAnimation(WalkAnimation);
        }

        private void Update()
        {
            Debug.Log(_childObjectTransform.rotation);
            
            
            if (_movement == false)
                return;
            
            if (_transform.position != _points[_currentPoint].position)
            {
                _transform.position =
                    Vector3.MoveTowards(_transform.position, _points[_currentPoint].position, _speed * Time.deltaTime);
            }
            else
            {
                _movement = false;
                SetAnimation(IdleAnimation);
                _ = StartCoroutine(PauseBetweenMovement());
            }
        }

        private IEnumerator PauseBetweenMovement()
        {
            yield return new WaitForSeconds(_pause);
            _currentPoint = _currentPoint < _points.Length - 1 ? ++_currentPoint : 0;
            RotateObject(_points[_currentPoint].position);
            SetAnimation(WalkAnimation);
            _movement = true;
        }

        private void SetAnimation(int animationClip) =>
            _animator.SetTrigger(animationClip);

        public void AttackCharacter(Transform character)
        {
            StopAllCoroutines();
            RotateObject(character.transform.position);
            SetAnimation(AttackAnimation);
            _movement = false;
        }

        private void RotateObject(Vector3 point)
        {
            _childObjectTransform.rotation = _transform.position.x > point.x
                ? Quaternion.Euler(Vector3.zero)
                : Quaternion.Euler(_moveToLeft);
        }
    }
}