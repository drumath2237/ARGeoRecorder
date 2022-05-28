using TMPro;
using UnityEngine;

namespace ARRecorder
{
    public class RecorderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playButtonText = null;
        [SerializeField] private TextMeshProUGUI recordButtonText = null;

        [SerializeField] private ARSessionRecorder arSessionRecorder;

        private void OnEnable()
        {
            if (!arSessionRecorder)
            {
                return;
            }

            arSessionRecorder.OnRecordingStatusChanged += UpdateText_OnRecordingStatusChanged;
            arSessionRecorder.OnPlaybackStatusChanged += UpdateText_OnPlaybackStatusChanged;
        }

        private void OnDisable()
        {
            if (!arSessionRecorder)
            {
                return;
            }

            arSessionRecorder.OnRecordingStatusChanged -= UpdateText_OnRecordingStatusChanged;
            arSessionRecorder.OnPlaybackStatusChanged -= UpdateText_OnPlaybackStatusChanged;
        }

        void UpdateText_OnRecordingStatusChanged(bool isRecording)
        {
            if (!recordButtonText)
            {
                return;
            }

            recordButtonText.text = isRecording ? "Stop Recording" : "Start Recording";
        }

        void UpdateText_OnPlaybackStatusChanged(bool isPlayback)
        {
            if (!playButtonText)
            {
                return;
            }

            playButtonText.text = isPlayback ? "Stop Playback" : "Start Playback";
        }

        public void OnRecordingButtonClicked()
        {
            arSessionRecorder.ToggleRecording();
        }

        public void OnPlaybackButtonClicked()
        {
            arSessionRecorder.TogglePlayback();
        }
    }
}