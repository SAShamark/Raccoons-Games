using System;
using UnityEngine;

namespace Inputs
{
    public abstract class InputSystem
    {
        public event Action<Vector3> OnDrag;
        public event Action OnEndInput;

        protected Ray Ray;
        protected readonly Camera Camera;

        private Plane _plane = new(Vector3.down, 0);
        private Vector3 _inputPosition;
        private bool _isInput;


        protected InputSystem()
        {
            Camera = Camera.main;
        }

        public abstract void Update();
        
        protected void BeginInput()
        {
            _isInput = true;
        }

        protected void GetInputPosition()
        {
            if (_plane.Raycast(Ray, out float distance))
            {
                _inputPosition = Ray.GetPoint(distance);
                OnDrag?.Invoke(_inputPosition);
            }
        }

        protected void EndInput()
        {
            if (_isInput)
            {
                OnEndInput?.Invoke();
                _isInput = false;
            }
        }
    }
}