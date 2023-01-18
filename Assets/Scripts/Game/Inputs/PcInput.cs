using UnityEngine;

namespace Game.Inputs
{
    public class PcInput : InputSystem
    {
        public override void Update()
        {
            Ray = Camera.ScreenPointToRay(Input.mousePosition);
            
            if (Input.GetMouseButtonDown(0))
            {
                BeginInput();
            }

            if (Input.GetMouseButton(0))
            {
                GetInputPosition();
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndInput();
            }
        }
    }
}