using System;
using Cube.CubeObject;
using Inputs;
using UnityEngine;

namespace Cube
{
    public class InputCubeAction
    {
        public event Action<CubeControl> Detached;

        private InputSystem _inputSystem;
        private CubeControl _cubeControl;

        public void Start()
        {
            _inputSystem = GetInputSystem();
        }

        public void Update()
        {
            _inputSystem?.Update();
        }
        
        public void Attach(CubeControl cubeControl)
        {
            _cubeControl = cubeControl;
            _inputSystem.OnDrag += DragObject;
            _inputSystem.OnEndInput += EndInputSystemObject;

            _cubeControl.IsDetached(false);
        }

        private void Detach()
        {
            _inputSystem.OnDrag -= DragObject;
            _inputSystem.OnEndInput -= EndInputSystemObject;

            var cube = _cubeControl;
            _cubeControl = null;

            cube.CubeMove.Push();
            cube.IsDetached(true);
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

        private InputSystem GetInputSystem()
        {
            return Application.platform switch
            {
                RuntimePlatform.WindowsPlayer => new PcInput(),
                RuntimePlatform.Android or RuntimePlatform.IPhonePlayer => new TouchInput(),
                _ => new PcInput()
            };
        }
    }
}