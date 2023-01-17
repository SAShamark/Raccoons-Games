using System;
using Cube;
using UnityEngine;

namespace Input
{
    public class TouchCubeAction
    {
        public event Action<CubeControl> Detached;

        private TouchInput _touchInput;
        private CubeControl _cubeControl;

        public void Update()
        {
            _touchInput?.Update();
        }

        public void Attach(CubeControl cubeControl)
        {
            _cubeControl = cubeControl;

            _touchInput = new TouchInput();
            _touchInput.OnDrag += DragObject;
            _touchInput.OnEndTouch += EndTouchObject;
            _cubeControl.IsDetached(false);
        }

        private void Detach()
        {
            _touchInput.OnDrag -= DragObject;
            _touchInput.OnEndTouch -= EndTouchObject;
            _touchInput = null;

            var cube = _cubeControl;
            _cubeControl = null;

            cube.IsDetached(true);
            cube.Push();

            Detached?.Invoke(cube);
        }

        private void DragObject(Vector3 position)
        {
            _cubeControl.gameObject.transform.position = new Vector3(position.x,
                _cubeControl.gameObject.transform.position.y, _cubeControl.gameObject.transform.position.z);
        }

        private void EndTouchObject()
        {
            Detach();
        }
    }
}