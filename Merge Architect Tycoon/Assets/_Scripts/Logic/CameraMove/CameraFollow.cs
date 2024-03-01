using UnityEngine;

namespace _Scripts.Logic.CameraMove
{
    public class CameraFollow : MonoBehaviour
    {
        [HideInInspector] public Transform target; 
        public float lerpSpeed = 0.1f; 
        public Vector3 offset = new Vector3(0, 10, -5); 

        private void LateUpdate()
        {
            if (target == null)
                return;

            Vector3 desiredPosition = target.position + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed);

            transform.position = smoothedPosition;
        }

        public void SetTarget(Transform _target)
        {
            target = _target;
        }
    }
}