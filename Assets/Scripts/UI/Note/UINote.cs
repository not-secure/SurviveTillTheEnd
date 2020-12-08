using TMPro;
using UnityEngine;

namespace UI.Note {
    public class UINote: MonoBehaviour {
        public TextMeshProUGUI content;
        public TextMeshProUGUI dayCounter;

        public void SetDay(int day) {
            dayCounter.text = day.ToString();
            content.text = (
                Resources.Load<TextAsset>("Texts/Notes/day_" + day) ??
                Resources.Load<TextAsset>("Texts/Notes/day_default")
            ).text;
        }
    }
}