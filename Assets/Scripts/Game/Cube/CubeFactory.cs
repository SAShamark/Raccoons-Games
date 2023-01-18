using Game.Cube.CubeObject;
using Game.Scriptable;
using UnityEngine;

namespace Game.Cube
{
    public class CubeFactory
    {
        private readonly ObjectPool<CubeControl> _cubePool;
        private readonly CubeDates _cubeDates;
        private readonly Transform _startCubePosition;

        private const int StartCubeCount = 1;

        public CubeFactory(CubeDates cubeDates, CubeControl cubePrefab, Transform cubeTransform,
            Transform startCubePosition)
        {
            _cubePool = new ObjectPool<CubeControl>(cubePrefab, StartCubeCount, cubeTransform);
            _cubeDates = cubeDates;
            _startCubePosition = startCubePosition;
        }

        public CubeControl GetCube()
        {
            var cube = _cubePool.GetFreeElement();
            cube.InitializeCube(_cubeDates, TryGiveSecondLevel());
            cube.CubeMove.MoveToStartPosition(_startCubePosition, cube.transform);
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