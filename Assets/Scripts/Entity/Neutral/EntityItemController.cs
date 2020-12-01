using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity.Neutral {
    public class EntityItemController : MonoBehaviour {
        public GameObject contentObject;
        public GameObject caseObject;
        public float friction = 0.01f;
        public float gravity = 9f;
        
        private Vector3 _motion = Vector3.zero;
        private float _minY;
        private bool _updateMotion = false;

        private void Update() {
            var position = transform.position;
            if (_updateMotion) {
                position += _motion * (5 * Time.deltaTime);
                _motion *= 1 - friction;
            
                if (_motion.y > -10)
                    _motion.y -= gravity * Time.deltaTime;
            }
            
            if (_updateMotion && position.y <= _minY) {
                _updateMotion = false;
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
            _minY = transform.position.y;
            _motion = new Vector3(
                Random.value * 5f - 2.5f,
                Random.value * 2.5f + 5f,
                Random.value * 5f - 2.5f
            );
            _updateMotion = true;
        }

        public void SetTexture(Texture tex) {
            var material = contentObject.GetComponent<Renderer>().material;
            material.mainTexture = tex;
        }
    }
}