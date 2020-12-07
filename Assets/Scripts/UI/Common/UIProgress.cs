using TMPro;
using UnityEngine;

namespace UI.Common {
    public class UIProgress: MonoBehaviour {
        public GameObject background;
        public TextMeshProUGUI text;

        public void SetProgress(float progress) {
            var transformLocalScale = background.transform.localScale;
            transformLocalScale.x = progress;

            background.transform.localScale = transformLocalScale;
        }

        public void SetText(string str) {
            text.SetText(str);
        }
    }
}