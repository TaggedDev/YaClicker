using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    public class ClickText : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform rectTransform;

        public RectTransform RectTransform => rectTransform;
        public Image Image => image;
        public TextMeshProUGUI Text => text;
    }
}