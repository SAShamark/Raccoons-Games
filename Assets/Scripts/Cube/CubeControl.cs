using System;
using System.Collections.Generic;
using DG.Tweening;
using Scriptable;
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


        private float _durationToStartPosition = 0.5f;
        private float _durationToChangeCube = 1f;
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

        public void InitializeCube(CubeDates cubeDates, bool isSecondLevel)
        {
            _pushDirection = cubeDates.PushDirection;
            _pushPower = cubeDates.PushPower;
            _colors = cubeDates.CubeColors;
            _durationToStartPosition = cubeDates.DurationToStartPosition;
            _durationToChangeCube = cubeDates.DurationToChangeCube;
            IsSecondLevel(isSecondLevel, cubeDates.BaseNumber);
        }

        public void MoveToStartPosition(Transform startPosition)
        {
            transform.DOMove(startPosition.position, _durationToStartPosition);
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
            _renderer.material.DOColor(color, _durationToChangeCube);
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