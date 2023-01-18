using System;
using Inputs;
using UnityEngine;

namespace Cube
{
    public class InputCubeAction
    {
        public event Action<CubeControl> Detached;

        private InputSystem _inputSystem;
        private CubeControl _cubeControl;


        public void Update()
        {
            _inputSystem?.Update();
        }

        public void Attach(CubeControl cubeControl)
        {
            _cubeControl = cubeControl;

            CheckingPlatformForInputSystem();

            _inputSystem.OnDrag += DragObject;
            _inputSystem.OnEndInput += EndInputSystemObject;
            _cubeControl.IsDetached(false);
        }

        private void Detach()
        {
            _inputSystem.OnDrag -= DragObject;
            _inputSystem.OnEndInput -= EndInputSystemObject;
            _inputSystem = null;

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

        private void EndInputSystemObject()
        {
            Detach();
        }

        private void CheckingPlatformForInputSystem()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer or RuntimePlatform.WindowsPlayer:
                    _inputSystem = new PcInput();
                    break;

                case RuntimePlatform.Android or RuntimePlatform.IPhonePlayer:
                    _inputSystem = new TouchInput();
                    break;

                default:
                    _inputSystem = new PcInput();
                    break;
            }
        }
    }
}