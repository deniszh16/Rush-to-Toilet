﻿using System;
using Logic.Sounds;
using UnityEngine;

namespace Logic.Levels
{
    public class Character : MonoBehaviour
    {
        [Header("Цвет персонажа")]
        [SerializeField] private ObjectColor _objectColor;
        
        [Header("Скорость движения")]
        [SerializeField] private float _speed;
        
        [Header("Компоненты персонажа")]
        [SerializeField] private Animator _animator;
        [SerializeField] private DrawWithMouse _drawWithMouse;
        [SerializeField] private BoxCollider2D _boxCollider;

        [Header("Дым столкновения")]
        [SerializeField] private ParticleSystem _clash;
        
        [Header("Звук шагов")]
        [SerializeField] private PlayingSound _playingSound;

        public ObjectColor ObjectColor => _objectColor;
        public DrawWithMouse DrawWithMouse => _drawWithMouse;

        private bool _movement;
        private Vector3[] _positions;
        private int _moveIndex;

        private Transform _parent;
        private Vector3 _moveToLeft;
        
        private bool _dive;
        private readonly float _diveSpeed = 3f;
        private Vector3 _divingPosition;

        public event Action Dived;
        public event Action<GameObject> Faced;

        private void Start()
        {
            _parent = transform.parent;
            _moveToLeft = new(0f, -180f, 0f);
        }

        private void Update()
        {
            if (_movement)
            {
                Vector2 currentPosition = _positions[_moveIndex];
                _parent.position = Vector2.MoveTowards(_parent.position, 
                    currentPosition, _speed * Time.deltaTime);

                _parent.rotation = currentPosition.x > _parent.position.x
                    ? Quaternion.Euler(Vector3.zero)
                    : Quaternion.Euler(_moveToLeft);
                
                float distance = Vector2.Distance(currentPosition, _parent.position);
                if (distance < 0.05f) _moveIndex++;

                if (_moveIndex > _positions.Length - 1)
                {
                    _movement = false;
                    _playingSound.StopSound();
                    _boxCollider.enabled = false;
                    SetAnimation(CharacterAnimations.Victory);
                }
            }

            if (_dive)
            {
                _parent.position = Vector2.MoveTowards(_parent.position, 
                    _divingPosition, _diveSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Character character))
            {
                if (ObjectColor != character.ObjectColor)
                    CollisionWithAnObstacle(character.gameObject);
            }

            if (col.TryGetComponent(out Obstacle obstacle))
                CollisionWithAnObstacle(obstacle.gameObject);

            if (col.TryGetComponent(out Plunger plunger))
            {
                if (plunger.ObjectColor == ObjectColor)
                    plunger.ActivatePlunger();
            }

            if (col.TryGetComponent(out Animal animal))
            {
                animal.AttackCharacter(gameObject.transform);
                CollisionWithAnObstacle(animal.gameObject);
            }
        }

        private void CollisionWithAnObstacle(GameObject collision)
        {
            _movement = false;
            _clash.gameObject.SetActive(true);
            _clash.Play();
            SetAnimation(CharacterAnimations.Damage);
            _playingSound.StopSound();
            Faced?.Invoke(collision);
        }

        public void SetArrayOfPoints()
        {
            _moveIndex = 0;
            _positions = new Vector3[_drawWithMouse.LineRenderer.positionCount];
            _drawWithMouse.LineRenderer.GetPositions(_positions);
        }

        public void StartMovement()
        {
            _movement = true;
            _playingSound.PlaySound();
            SetAnimation(CharacterAnimations.Run);
        }

        public void StartDive()
        {
            _dive = true;
            Dived?.Invoke();
        }

        private void SetAnimation(CharacterAnimations anim) =>
            _animator.SetTrigger(anim.ToString());

        public void SetDivingPosition(Vector3 position) =>
            _divingPosition = position;

        private void OnDestroy() =>
            Dived = null;
    }
}