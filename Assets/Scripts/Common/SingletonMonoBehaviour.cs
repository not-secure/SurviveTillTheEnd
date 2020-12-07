using UnityEngine;

namespace Common {
    public class SingletonMonoBehaviour : MonoBehaviour {
        
        private static SingletonMonoBehaviour _self;

        private void Start() {
            _self = this;
        }

        public static SingletonMonoBehaviour GetInstance() {
            return _self;
        }
    }
}
