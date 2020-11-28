using UnityEngine;

namespace Common {
    public class CameraController : MonoBehaviour
    {
        public GameObject cameraObject;
        public GameObject targetObject;

        public Vector3 offset;

        public void Start()
        {
            if (cameraObject == null)
                cameraObject = this.gameObject;
        }

        public void Update()
        {
            Vector3 updatedPosition = targetObject.transform.position;
            updatedPosition += offset;

            cameraObject.transform.position = updatedPosition;
        }
    }
}
