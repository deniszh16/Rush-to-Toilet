using UnityEngine;

namespace Logic.Levels
{
    public class Cerberus : Animal
    {
        [Header("Цель монстра")]
        [SerializeField] private Character _character;

        protected override void Start()
        {
            base.Start();
            _character.Moving += StartMovement;
            _character.Dived += StopMovement;
        }

        private void Update()
        {
            if (_movement == false)
                return;
            
            float distance = Vector2.Distance(_transform.position, _character.transform.parent.position);
            
            if (distance > 0.05f)
            {
                _transform.position = Vector2.MoveTowards(_transform.position,
                    _character.transform.parent.position, _speed * Time.deltaTime);
            }
        }

        private void StartMovement()
        {
            _movement = true;
            SetAnimation(WalkAnimation);
        }

        private void StopMovement()
        {
            _movement = false;
            SetAnimation(IdleAnimation);
        }

        private void OnDestroy()
        {
            _character.Moving -= StartMovement;
            _character.Dived -= StopMovement;
        }
    }
}