using UnityEngine;

namespace Logic.UI
{
    [RequireComponent(typeof(Camera))]
    public class AspectRatio : MonoBehaviour
    {
        private Camera _camera;
        private float _aspectRatio;
        private const float _cameraSize = 6.2f;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _aspectRatio = _camera.aspect;
        }

        private void Start()
        {
            if (_aspectRatio <= 0.5f)
                _camera.orthographicSize = _cameraSize;
        }
    }
}