using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Rigidbody))]
    public class CubeControl : MonoBehaviour
    {
        public event Action<CubeControl, CubeControl> OnCollide;
        public int Number { get; private set; }
        public int ColorIndex { get; private set; }

        [SerializeField] private List<TMP_Text> _numberTexts;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;


        private List<Color> _colors;
        private Vector3 _pushDirection;
        private float _pushPower;
        private bool _isDetach;


        private const int NextNumber = 2;


        private void Update()
        {
            if (transform.position.x is < -5.5f or > 5.5f)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CubeControl cubeControl) && cubeControl._isDetach &&
                cubeControl.Number == Number)
            {
                CollideWithSameNumberCube(cubeControl);
            }
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
        }

        public void InitializeCube(Vector3 pushDirection, float pushPower, int baseNumber, List<Color> colors,
            bool isSecondLevel)
        {
            _pushDirection = pushDirection;
            _pushPower = pushPower;
            _colors = colors;

            IsSecondLevel(isSecondLevel, baseNumber);
        }

        private void IsSecondLevel(bool isSecondLevel, int baseNumber)
        {
            if (isSecondLevel)
            {
                ChangeNumber(baseNumber * NextNumber);
                ChangeColorIndex(1);
            }
            else
            {
                ChangeColorIndex(0);
                ChangeNumber(baseNumber);
            }
        }

        public void ChangeNumber(int number)
        {
            Number = number;
            SetNumberText(number);
        }

        public void ChangeColorIndex(int index)
        {
            ColorIndex = index;
            SetColor(_colors.Count > index ? _colors[index] : _colors[_colors.Count - 1]);
        }

        private void SetNumberText(int number)
        {
            foreach (var text in _numberTexts)
            {
                text.text = number.ToString();
            }
        }

        private void SetColor(Color color)
        {
            _renderer.material.color = color;
        }

        public void IsDetached(bool detached)
        {
            _isDetach = detached;

            _rigidbody.constraints = _isDetach ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
        }

        public void Push()
        {
            _rigidbody.AddForce(_pushDirection * _pushPower, ForceMode.Impulse);
        }

        private void CollideWithSameNumberCube(CubeControl cubeControl)
        {
            OnCollide?.Invoke(this, cubeControl);
        }
    }
}