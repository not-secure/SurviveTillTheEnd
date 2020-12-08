using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common {
    public class CameraController : MonoBehaviour
    {
        public GameObject cameraObject;
        public GameObject targetObject;
        public Material hiddenMaterial;

        public Vector3 offset;

        private readonly RaycastHit[] _raycastBuffer = new RaycastHit[64];
        private readonly Renderer[] _hidings = new Renderer[64];
        private readonly Material[][] _hiddenMaterials = new Material[64][];
        private int _raycastSize = 0;
        
        public void Start() {
            if (cameraObject == null)
                cameraObject = this.gameObject;
        }

        public void Update() {
            var cameraPosition = cameraObject.transform.position;
            var playerPosition = targetObject.transform.position;

            for (var i = 0; i < _raycastSize; i++) {
                if (_hidings[i])
                    _hidings[i].materials = _hiddenMaterials[i];
            }

            var displacement = playerPosition - cameraPosition;
            _raycastSize = Physics.RaycastNonAlloc(
                cameraPosition, 
                displacement,
                _raycastBuffer,
                displacement.magnitude
            );

            for (var i = 0; i < _raycastSize; i++) {
                var hidingRenderer = _raycastBuffer[i].collider.GetComponent<Renderer>();
                if (!hidingRenderer) continue;
                
                _hidings[i] = hidingRenderer;
                _hiddenMaterials[i] = hidingRenderer.materials;
                hidingRenderer.material = hiddenMaterial;
            }
            
            var updatedPosition = playerPosition;
            updatedPosition += offset;

            cameraPosition = updatedPosition;
            cameraObject.transform.position = cameraPosition;
        }
    }
}
