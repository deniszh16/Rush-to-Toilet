using UnityEngine;

namespace Logic.Levels
{
    public class DrawWithMouse : MonoBehaviour
    {
        [Header("Основная камера")]
        [SerializeField] private Camera _mainCamera;

        [Header("Компонент отрисовки линии")]
        [SerializeField] private LineRenderer _line;

        [Header("Ширина линии")]
        [SerializeField] private float _width;

        public LineRenderer LineRenderer => _line;
        
        public bool DrawingActivity { get; set; }

        private Vector3 _previousPosition;
        private Vector3 _currentPosition;
        
        private const float MinDistance = 0.1f;

        private void Start()
        {
            _previousPosition = transform.position;
            _line.startWidth = _line.endWidth = _width;
            _line.positionCount = 1;
        }

        private void Update()
        {
            if (DrawingActivity == false)
                return;

            if (Input.GetMouseButton(0))
                CreateLine();
        }

        private void CreateLine()
        {
            _currentPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _currentPosition.z = 0;
            
            if (Vector3.Distance(_currentPosition, _previousPosition) > MinDistance)
            {
                if (_previousPosition == transform.position)
                {
                    _line.SetPosition(0, _currentPosition);
                }
                else
                {
                    _line.positionCount++;
                    _line.SetPosition(_line.positionCount - 1, _currentPosition);
                }
                    
                _previousPosition = _currentPosition;
            }
        }
    }
}