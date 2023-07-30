using System.Collections;
using UnityEngine;

namespace Logic.Levels
{
    public class Wolf : Animal
    {
        [Header("Точки для перемещения")]
        [SerializeField] private Transform[] _points;
        
        [Header("Пауза при повороте")]
        [SerializeField] private float _pause;
        
        private int _currentPoint;

        protected override void Start()
        {
            base.Start();
            _movement = true;
            _currentPoint = 0;
            SetAnimation(WalkAnimation);
        }

        private void Update()
        {
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
    }
}