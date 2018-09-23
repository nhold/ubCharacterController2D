using UnityEngine;

namespace Bifrost
{
    namespace ubCharacterMotor2D
    {
        /// <summary>
        /// This handles the movement of a character or unit.
        /// It uses the BoxCollider2D to handle collisions and keeps track
        /// of it's last direction for use with animations etc.
        /// </summary>
        public class CharacterMotor2D : MonoBehaviour
        {
            [SerializeField]
            private LayerMask collisionLayerMask;

            [SerializeField]
            private BoxCollider2D boxCollider2D;

            private Vector2 origin;
            private Vector2 directionX;
            private Vector2 directionY;
            private Vector3 directionVector;

            private Vector3 lastDirection = new Vector3(1, 0);
            public Vector3 LastDirection
            {
                get
                {
                    return lastDirection;
                }

                set
                {
                    lastDirection = value;
                }
            }

            /// <summary>
            /// Move will move the character in a direction by speed amount.
            /// it will also update the `LastDirection` property so that you
            /// may update any required direction related things.
            /// </summary>
            /// <param name="direction">The direction to move.</param>
            /// <param name="speed">The speed at which to move in units\per second.</param>
            public void Move(Vector2 direction, float speed)
            {
                if (direction != Vector2.zero)
                {
                    LastDirection = direction.normalized;
                }

                directionVector = GetScaledMovementVector(direction, speed);

                origin.x = transform.position.x + boxCollider2D.offset.x;
                origin.y = transform.position.y + boxCollider2D.offset.y;

                directionX.x = direction.x;
                directionY.y = direction.y;

                RaycastHit2D hitInformationX = Physics2D.BoxCast(origin, boxCollider2D.size, 0, directionX, speed * Time.deltaTime, collisionLayerMask);
                RaycastHit2D hitInformationY = Physics2D.BoxCast(origin, boxCollider2D.size, 0, directionY, speed * Time.deltaTime, collisionLayerMask);

                if (hitInformationX.transform != null)
                {
                    var dis = hitInformationX.collider.Distance(boxCollider2D);
                    directionVector.x = direction.normalized.x * dis.distance;
                }

                if (hitInformationY.transform != null)
                {
                    var dis = hitInformationY.collider.Distance(boxCollider2D);
                    directionVector.y = direction.normalized.y * dis.distance;
                }

                transform.Translate(directionVector);
            }

            /// <summary>
            /// Push will move a character but not affect it's last direction.
            /// </summary>
            /// <param name="direction">The direction to move.</param>
            /// <param name="speed">The speed at which you want to move at units\per second.</param>
            public void Push(Vector2 direction, float speed)
            {
                directionVector = GetScaledMovementVector(direction, speed);

                origin.x = transform.position.x + boxCollider2D.offset.x;
                origin.y = transform.position.y + boxCollider2D.offset.y;

                directionX.x = direction.x;
                directionY.y = direction.y;

                RaycastHit2D hitInformationX = Physics2D.BoxCast(origin, boxCollider2D.size, 0, directionX, speed * Time.deltaTime, collisionLayerMask);
                RaycastHit2D hitInformationY = Physics2D.BoxCast(origin, boxCollider2D.size, 0, directionY, speed * Time.deltaTime, collisionLayerMask);

                if (hitInformationX.transform != null)
                {
                    var dis = hitInformationX.collider.Distance(boxCollider2D);
                    directionVector.x = direction.normalized.x * dis.distance;
                }

                if (hitInformationY.transform != null)
                {
                    var dis = hitInformationY.collider.Distance(boxCollider2D);
                    directionVector.y = direction.normalized.y * dis.distance;
                }

                transform.Translate(directionVector);
            }

            private void Awake()
            {
                if (boxCollider2D == null)
                {
                    boxCollider2D = GetComponent<BoxCollider2D>();
                    if (boxCollider2D == null)
                    {
                        boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
                    }
                }
            }

            private Vector3 GetScaledMovementVector(Vector2 direction, float speed)
            {
                return (direction.normalized * speed) * Time.deltaTime;
            }
        }

    }
}
