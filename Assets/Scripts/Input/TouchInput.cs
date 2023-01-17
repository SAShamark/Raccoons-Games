using System;
using UnityEngine;

namespace Input
{
    public class TouchInput
    {
        public event Action<Vector3> OnDrag;
        public event Action OnEndTouch;


        private Plane _plane = new(Vector3.down, 0);
        private Vector3 _touchPosition;
        private Ray _ray;
        private bool _isTouch;

        private readonly Camera _camera;

        public TouchInput()
        {
            _camera = Camera.main;
        }

        public void Update()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.GetTouch(0);

                _ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _isTouch = true;
                        break;
                    
                    case TouchPhase.Moved:
                        GetTouchPosition();
                        break;
                    
                    case TouchPhase.Ended:
                        EndTouch();
                        break;
                }
            }
        }

        private void GetTouchPosition()
        {
            if (_plane.Raycast(_ray, out float distance))
            {
                _touchPosition = _ray.GetPoint(distance);
                OnDrag?.Invoke(_touchPosition);
            }
        }

        private void EndTouch()
        {
            if (_isTouch)
            {
                OnEndTouch?.Invoke();
                _isTouch = false;
            }
        }
    }
}