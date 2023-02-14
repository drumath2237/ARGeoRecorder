using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;

#if UNITY_ANDROID
using UnityEngine.XR.ARCore;
#endif


namespace ARRecorder
{
    public class ARSessionRecorder : MonoBehaviour
    {
        [SerializeField] private ARSession arSession;

        private string _mp4Path;

        public event Action<bool> OnRecordingStatusChanged;
        public event Action<bool> OnPlaybackStatusChanged;

        public event Action<string> OnSendMessage;


        private void Awake()
        {
            _mp4Path = Path.Combine(Application.persistentDataPath, "arcore-session.mp4");
        }

        public void ToggleRecording()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                OnSendMessage?.Invoke("!!!AR Session is not ARCore Session");
                return;
            }

            if (subsystem.recordingStatus.Recording())
            {
                subsystem.StopRecording();
                OnRecordingStatusChanged?.Invoke(false);

                OnSendMessage?.Invoke($"Stopped recording and saved to {_mp4Path}");

                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
                OnSendMessage?.Invoke("Stopped playback");
                OnPlaybackStatusChanged?.Invoke(false);
            }

            using var recordingConfig = new ArRecordingConfig(subsystem.session);
            _mp4Path = Path.Combine(Application.persistentDataPath,
                $"arcore-session{DateTime.Now:yyyyMMddHHHmmss}.mp4");
            recordingConfig.SetMp4DatasetFilePath(subsystem.session, _mp4Path);

            var screenRotation = Screen.orientation switch
            {
                ScreenOrientation.Portrait => 0,
                ScreenOrientation.LandscapeLeft => 90,
                ScreenOrientation.PortraitUpsideDown => 180,
                ScreenOrientation.LandscapeRight => 270,
                _ => 0
            };
            recordingConfig.SetRecordingRotation(subsystem.session, screenRotation);

            subsystem.StartRecording(recordingConfig);

            OnRecordingStatusChanged?.Invoke(true);

            OnSendMessage?.Invoke("Start recording");

#endif
        }


        public void TogglePlayback()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                OnSendMessage?.Invoke("!!!AR Session is not ARCore Session");
                return;
            }

            if (subsystem.playbackStatus.Playing())
            {
                subsystem.StopPlayback();
                OnPlaybackStatusChanged?.Invoke(false);
                OnSendMessage?.Invoke("Stopped playback");
                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
                OnPlaybackStatusChanged?.Invoke(false);
                OnSendMessage?.Invoke("Stopped playback");

                return;
            }

            if (!File.Exists(_mp4Path))
            {
                OnSendMessage?.Invoke($"!!!cannot find file path {_mp4Path}");
                return;
            }

            subsystem.StartPlayback(_mp4Path);

            OnPlaybackStatusChanged?.Invoke(true);

            OnSendMessage?.Invoke("Start playback");
#endif
        }
    }
}