using UnityEngine;

namespace _Scripts.Logic.CameraMove
{
    public class MoveCameraWithTouch : MonoBehaviour
    {
        public float dragSpeed = 2f;
        private Vector3 dragOrigin;
        private Vector3 lastMousePosition;

        void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                lastMousePosition = dragOrigin;
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentMousePosition = Input.mousePosition;
                Vector3 difference = currentMousePosition - lastMousePosition;
                lastMousePosition = currentMousePosition;

                Vector3 move = new Vector3(-difference.x, -difference.y, 0) * dragSpeed * Time.deltaTime;

                transform.Translate(move, Space.World);
            }
        }
    }
}