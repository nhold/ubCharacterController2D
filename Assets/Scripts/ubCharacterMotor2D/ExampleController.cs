using UnityEngine;

namespace Bifrost
{
    namespace ubCharacterMotor2D.Example
    {
        public class ExampleController : MonoBehaviour
        {
            [SerializeField]
            private CharacterMotor2D characterMotor2D;

            [SerializeField]
            private float speed = 1.0f;

            private Vector2 lastInput;

            private const string HORIZONTAL_AXIS = "Horizontal";
            private const string VERTICAL_AXIS = "Vertical";

            private void Update()
            {
                lastInput.x = Input.GetAxisRaw(HORIZONTAL_AXIS);
                lastInput.y = Input.GetAxisRaw(VERTICAL_AXIS);

                characterMotor2D.Move(lastInput, speed);
            }
        }

    }
}
