using UnityEngine;

namespace UI.Common {
    public class UIProgress: MonoBehaviour {
        public GameObject background;

        public void SetProgress(float progress) {
            var transformLocalScale = background.transform.localScale;
            transformLocalScale.x = progress;

            background.transform.localScale = transformLocalScale;
        }
    }
}