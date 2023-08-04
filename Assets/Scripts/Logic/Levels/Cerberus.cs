using System.Collections;
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
            if (Movement == false)
                return;
            
            float distance = Vector2.Distance(_transform.position, _character.transform.parent.position);
            
            if (distance > 0.05f)
            {
                _transform.position = Vector2.MoveTowards(_transform.position,
                    _character.transform.parent.position, _speed * Time.deltaTime);
            }
            else
            {
                StopMovement();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Obstacle>())
            {
                Movement = false;
                _capsuleCollider.enabled = false;
                SetAnimation(DeadAnimation);
            }
        }

        private void StartMovement()
        {
            Movement = true;
            SetAnimation(WalkAnimation);
            _ = StartCoroutine(RotateObjectCoroutine());
        }

        private void StopMovement()
        {
            Movement = false;
            SetAnimation(IdleAnimation);
            StopAllCoroutines();
        }
        
        private IEnumerator RotateObjectCoroutine()
        {
            WaitForSeconds seconds = new WaitForSeconds(0.1f);
            
            while (Movement)
            {
                yield return seconds;
                RotateObject(_character.transform.position);
            }
        }

        private void OnDestroy()
        {
            _character.Moving -= StartMovement;
            _character.Dived -= StopMovement;
        }
    }
}