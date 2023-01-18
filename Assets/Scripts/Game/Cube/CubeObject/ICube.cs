using System;
using Game.Scriptable;

namespace Game.Cube.CubeObject
{
    public interface ICube
    {
        public event Action<CubeControl, CubeControl> OnCollide;
        public CubeLevel CubeLevel { get; }
        public CubeMove CubeMove { get; }
        public void InitializeCube(CubeDates cubeDates, bool isSecondLevel);
    }
}