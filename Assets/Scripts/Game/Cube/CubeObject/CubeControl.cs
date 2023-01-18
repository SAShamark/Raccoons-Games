using System;
using System.Collections.Generic;
using Game.Scriptable;
using TMPro;
using UnityEngine;

namespace Game.Cube.CubeObject
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Rigidbody))]
    public class CubeControl : MonoBehaviour, ICube
    {
        public event Action<CubeControl, CubeControl> OnCollide;
        public CubeLevel CubeLevel { get; private set; }
        public CubeMove CubeMove { get; private set; }

        [SerializeField] private List<TMP_Text> _numberTexts;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _levelUpParticle;

        private bool _isDetach;

        private const float ZPositionForCombine = 3;

        private void Update()
        {
            CubeMove.ExitField(transform);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CubeControl cubeControl) && _isDetach &&
                cubeControl.CubeLevel.Number == CubeLevel.Number && transform.position.z > ZPositionForCombine)
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
            CubeLevel = new CubeLevel(cubeDates.CubeColors, _numberTexts, _renderer, cubeDates.DurationToChangeCube,
                _levelUpParticle);
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