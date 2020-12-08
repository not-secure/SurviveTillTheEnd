using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Neutral {
    public class EntityItemController : MonoBehaviour {
        public GameObject contentObject;
        public GameObject caseObject;
        
        [NonSerialized] public bool UpdateMotion = false;
        
        private Vector3 _motion = Vector3.zero;
        private float _minY;
        private Rigidbody _rigidbody;
        private Collider _collider;

        private void OnEnable() {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private float _staleTime = 0;
        
        private void Update() {
            var position = transform.position;
            
            if (UpdateMotion) {
                if (!Physics.Raycast(transform.position, -Vector3.up, 1f)) return;

                _staleTime += Time.deltaTime;
                if (_staleTime <= 1f) return;
                
                UpdateMotion = false;
                _rigidbody.isKinematic = true;
                _collider.isTrigger = true;
                _minY = transform.position.y;
            }

            position.y += Mathf.Sin(Time.time) * (Time.deltaTime / 3);
            position.y = Mathf.Max(_minY, position.y);
            transform.position = position;

            caseObject.transform.rotation = Quaternion.Euler(
                caseObject.transform.rotation.eulerAngles +
                new Vector3(0, Time.deltaTime * 10, 0)
            );
        }

        public void AddRandomMotion() {
            _motion = new Vector3(
                Random.value * 5f - 2.5f,
                Random.value * 2.5f + 5f,
                Random.value * 5f - 2.5f
            );
            _rigidbody.AddForce(_motion, ForceMode.Impulse);
            UpdateMotion = true;
        }

        public void SetTexture(Texture tex) {
            var material = contentObject.GetComponent<Renderer>().material;
            material.mainTexture = tex;
        }
    }
}