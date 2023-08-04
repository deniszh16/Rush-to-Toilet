using UnityEngine;

namespace Logic.Levels
{
    public class CharacterSelection : MonoBehaviour
    {
        [Header("Необходимые компоненты")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LevelTasks _levelTasks;

        [Header("Слой объектов")]
        [SerializeField] private LayerMask _layerMask;
        
        private const float RayLength = 30f;

        private RaycastHit2D _hit;
        private Character _activeCharacter;
        private bool _drawing;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hit = CheckCollision();
                SelectCharacter();
            }

            if (_drawing)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _hit = CheckCollision();
                    CheckFinish();
                }
            }
        }

        private RaycastHit2D CheckCollision()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 position = new Vector2(mousePosition.x, mousePosition.y);
            return Physics2D.Raycast(position, Vector2.zero, RayLength, _layerMask);
        }

        private void SelectCharacter()
        {  
            if (_hit.collider != null)
            {
                if (_hit.collider.TryGetComponent(out Character character))
                {
                    _activeCharacter = character;
                    if (_activeCharacter.CompletedRoute == false)
                    {
                        _drawing = true;
                        _activeCharacter.DrawWithMouse.DrawingActivity = true;
                    }
                }
            }
        }

        private void CheckFinish()
        {
            if (_hit.collider != null)
            {
                if (_hit.collider.TryGetComponent(out Toilet toilet))
                {
                    if (toilet.ObjectColor == _activeCharacter.ObjectColor)
                    {
                        _drawing = false;
                        toilet.SetAttachedCharacter(_activeCharacter);
                        _activeCharacter.CompletedRoute = true;
                        _activeCharacter.DrawWithMouse.DrawingActivity = false;
                        _activeCharacter.SetArrayOfPoints();
                        _activeCharacter.SetDivingPosition(toilet.transform.GetChild(1).position);
                        _activeCharacter.DrawWithMouse.RecordNumberOfPoints();
                        _levelTasks.AddLine();
                        return;
                    }
                }
            }
            
            _drawing = false;
            _activeCharacter.DrawWithMouse.DrawingActivity = false;
            _activeCharacter.DrawWithMouse.LineRenderer.positionCount = 0;
        }
    }
}