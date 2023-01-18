using UnityEngine;

namespace Inputs
{
    public class TouchInput : InputSystem
    {
        public override void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                Ray = Camera.ScreenPointToRay(Input.mousePosition);
                  
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        BeginInput();
                        break;

                    case TouchPhase.Moved:
                        GetInputPosition();
                        break;

                    case TouchPhase.Ended:
                        EndInput();
                        break;
                }
            }
        }
    }
}