using System;
using System.Collections.Generic;
using Scriptable;
using TMPro;
using UnityEngine;

namespace Cube.CubeObject
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Rigidbody))]
    public class CubeControl : MonoBehaviour
    {
        public event Action<CubeControl, CubeControl> OnCollide;
        private bool _isDetach;

        [SerializeField] private List<TMP_Text> _numberTexts;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;

        public CubeLevel CubeLevel { get; private set; }
        public CubeMove CubeMove { get; private set; }


        private void Update()
        {
            CubeMove.ExitField(transform);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CubeControl cubeControl) && _isDetach &&
                cubeControl.CubeLevel.Number == CubeLevel.Number && transform.position.z > 3)
            {
                OnCollide?.Invoke(this, cubeControl);
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
            CubeLevel = new CubeLevel(cubeDates.CubeColors, _numberTexts, _renderer, cubeDates.DurationToChangeCube);
            CubeMove = new CubeMove(cubeDates.PushDirection, cubeDates.DurationToStartPosition, cubeDates.PushPower,
                _rigidbody);

            CubeLevel.IsSecondLevel(isSecondLevel, cubeDates.BaseNumber);
        }

        public void IsDetached(bool detached)
        {
            _isDetach = detached;

            _rigidbody.constraints = _isDetach ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
        }
    }
}