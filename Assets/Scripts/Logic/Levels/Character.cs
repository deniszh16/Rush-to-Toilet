using System;
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

        [Header("Дым столкновения")]
        [SerializeField] private ParticleSystem _clash;

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
                _parent.transform.position = Vector2.MoveTowards(_parent.transform.position, 
                    currentPosition, _speed * Time.deltaTime);

                _parent.transform.rotation = currentPosition.x > _parent.transform.position.x
                    ? Quaternion.Euler(Vector3.zero)
                    : Quaternion.Euler(_moveToLeft);
                
                float distance = Vector2.Distance(currentPosition, _parent.transform.position);
                if (distance < 0.05f) _moveIndex++;

                if (_moveIndex > _positions.Length - 1)
                {
                    _movement = false;
                    SetAnimation(CharacterAnimations.Victory);
                }
            }

            if (_dive)
            {
                _parent.transform.position = Vector2.MoveTowards(_parent.transform.position, 
                    _divingPosition, _diveSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.TryGetComponent(out Character character))
            {
                if (ObjectColor != character.ObjectColor)
                {
                    _movement = false;
                    _clash.gameObject.SetActive(true);
                    _clash.Play();
                    SetAnimation(CharacterAnimations.Damage);
                }
            }
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