using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "CubeDates", menuName = "ScriptableObjects/Cube", order = 1)]
    public class CubeDates : ScriptableObject
    {
        [SerializeField] private List<Color> _cubeColors;
        [SerializeField] private int _baseNumber;
        [SerializeField] private float _forcePower;
        [SerializeField] private Vector3 _pushDirection;

        public List<Color> CubeColors => _cubeColors;
        public int BaseNumber => _baseNumber;
        public float PushPower => _forcePower;
        public Vector3 PushDirection => _pushDirection;
    }
}