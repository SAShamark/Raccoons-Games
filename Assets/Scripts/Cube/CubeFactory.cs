using Scriptable;
using UnityEngine;

namespace Cube
{
    public class CubeFactory
    {
        private readonly ObjectPool<CubeControl> _cubePool;
        private readonly CubeDates _cubeDates;
        private readonly Transform _startCubePosition;

        public CubeFactory(CubeDates cubeDates, CubeControl cubePrefab, Transform cubeTransform,
            Transform startCubePosition)
        {
            _cubePool = new ObjectPool<CubeControl>(cubePrefab, 1, cubeTransform);
            _cubeDates = cubeDates;
            _startCubePosition = startCubePosition;
        }

        public CubeControl GetCube()
        {
            var cube = _cubePool.GetFreeElement();
            cube.InitializeCube(_cubeDates, TryGiveSecondLevel());
            cube.MoveToStartPosition(_startCubePosition);
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