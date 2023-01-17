using Scriptable;
using UnityEngine;

namespace Cube
{
    public class CubeFactory
    {
        private readonly ObjectPool<CubeControl> _cubePool;
        private readonly CubeDates _cubeDates;

        public CubeFactory(CubeDates cubeDates, CubeControl cubePrefab, Transform cubeTransform)
        {
            _cubeDates = cubeDates;
            _cubePool = new ObjectPool<CubeControl>(cubePrefab, 1, cubeTransform);
        }

        public CubeControl GetCube()
        {
            var cube = _cubePool.GetFreeElement();
            cube.InitializeCube(_cubeDates.PushDirection, _cubeDates.PushPower, _cubeDates.BaseNumber,
                _cubeDates.CubeColors, TryGiveSecondLevel());
            return cube;
        }

        //75%(lvl1) - 25%(lvl2)
        private bool TryGiveSecondLevel()
        {
            int index = Random.Range(1, 5);
            return index == 1;
        }
    }
}