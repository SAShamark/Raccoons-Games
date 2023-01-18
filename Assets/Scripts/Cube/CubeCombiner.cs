using System;
using Cube.CubeObject;
using UnityEngine;

namespace Cube
{
    public class CubeCombiner
    {
        public event Action<CubeControl> OnCombined;

        public void Combine(CubeControl mainCubeControl, CubeControl secondCubeControl)
        {
            mainCubeControl.transform.position = GetMiddlePosition(mainCubeControl, secondCubeControl);


            int nextCubeNumber = mainCubeControl.CubeLevel.Number + secondCubeControl.CubeLevel.Number;
            int nextColorIndex = mainCubeControl.CubeLevel.ColorIndex + 1;
            mainCubeControl.CubeLevel.NextLevel(nextCubeNumber, nextColorIndex);


            mainCubeControl.IsDetached(false);
            secondCubeControl.IsDetached(false);

            Combined(mainCubeControl, secondCubeControl);
        }

        private void Combined(CubeControl mainCubeControl, CubeControl secondCubeControl)
        {
            mainCubeControl.IsDetached(true);
            secondCubeControl.gameObject.SetActive(false);
            OnCombined?.Invoke(mainCubeControl);
        }

        private Vector3 GetMiddlePosition(CubeControl mainCubeControl, CubeControl secondCubeControl)
        {
            var position1 = mainCubeControl.transform.position;
            var position2 = secondCubeControl.transform.position;

            var vector = position1 - position2;
            var halfVector = vector / 2f;
            var middlePosition = position2 + halfVector;

            return middlePosition;
        }
    }
}