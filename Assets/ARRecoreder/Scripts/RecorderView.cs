using TMPro;
using UnityEngine;

namespace ARRecorder
{
    public class RecorderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playButtonText = null;
        [SerializeField] private TextMeshProUGUI recordButtonText = null;

        private ARSessionRecorder _arSessionRecorder;
    }
}