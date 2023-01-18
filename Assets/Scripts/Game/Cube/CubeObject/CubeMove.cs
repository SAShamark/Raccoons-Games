using DG.Tweening;
using UnityEngine;

namespace Game.Cube.CubeObject
{
    public class CubeMove
    {
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _pushDirection;
        private readonly float _durationToStartPosition;
        private readonly float _pushPower;

        private const float LeftFieldEdge = -5;
        private const float RightFieldEdge = 5;

        public CubeMove(Vector3 pushDirection, float durationToStartPosition, float pushPower, Rigidbody rigidbody)
        {
            _pushDirection = pushDirection;
            _durationToStartPosition = durationToStartPosition;
            _pushPower = pushPower;
            _rigidbody = rigidbody;
        }

        public void ExitField(Transform transform)
        {
            switch (transform.position.x)
            {
                case < LeftFieldEdge:
                    float indentationFromLeftEdge = LeftFieldEdge + 1;
                    transform.DOMove(new Vector3(indentationFromLeftEdge, transform.position.y, transform.position.z),
                        _durationToStartPosition);
                    break;
                case > RightFieldEdge:
                    float indentationFromRightEdge = RightFieldEdge - 1;
                    transform.DOMove(new Vector3(indentationFromRightEdge, transform.position.y, transform.position.z),
                        _durationToStartPosition);
                    break;
            }
        }

        public void MoveToStartPosition(Transform startPosition, Transform transform)
        {
            transform.DOMove(startPosition.position, _durationToStartPosition);
        }

        public void Push()
        {
            _rigidbody.AddForce(_pushDirection * _pushPower, ForceMode.Impulse);
        }
    }
}