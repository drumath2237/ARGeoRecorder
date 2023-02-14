using TMPro;
using UnityEngine;

namespace ARRecorder
{
    public class RecorderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playButtonText;
        [SerializeField] private TextMeshProUGUI recordButtonText;

        [SerializeField] private TextMeshProUGUI logMessagesText;

        [SerializeField] private ARSessionRecorder arSessionRecorder;

        private void OnEnable()
        {
            if (!arSessionRecorder)
            {
                return;
            }

            arSessionRecorder.OnRecordingStatusChanged += UpdateText_OnRecordingStatusChanged;
            arSessionRecorder.OnPlaybackStatusChanged += UpdateText_OnPlaybackStatusChanged;
            arSessionRecorder.OnSendMessage += AddLogMessage_OnSendMessage;
        }

        private void OnDisable()
        {
            if (!arSessionRecorder)
            {
                return;
            }

            arSessionRecorder.OnRecordingStatusChanged -= UpdateText_OnRecordingStatusChanged;
            arSessionRecorder.OnPlaybackStatusChanged -= UpdateText_OnPlaybackStatusChanged;
            arSessionRecorder.OnSendMessage -= AddLogMessage_OnSendMessage;
        }

        private void UpdateText_OnRecordingStatusChanged(bool isRecording)
        {
            if (!recordButtonText)
            {
                return;
            }

            recordButtonText.text = isRecording ? "Stop Recording" : "Start Recording";
        }

        private void UpdateText_OnPlaybackStatusChanged(bool isPlayback)
        {
            if (!playButtonText)
            {
                return;
            }

            playButtonText.text = isPlayback ? "Stop Playback" : "Start Playback";
        }

        private void AddLogMessage_OnSendMessage(string message)
        {
            if (!logMessagesText)
            {
                return;
            }

            logMessagesText.text = message + "\n\n" + logMessagesText.text;
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