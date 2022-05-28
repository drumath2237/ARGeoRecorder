using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

#if UNITY_ANDROID
using System;
using UnityEngine.XR.ARCore;
#endif


namespace ARRecorder
{
    public class ARSessionRecorder : MonoBehaviour
    {
        [SerializeField] private ARSession arSession;

        private string mp4path = null;

        public event Action<bool> OnRecordingStatusChanged = null;
        public event Action<bool> OnPlaybackStatusChanged = null;

        public event Action<string> OnSendMessage = null;


        private void Awake()
        {
            mp4path = Path.Combine(Application.persistentDataPath, "arcore-session.mp4");
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
                
                OnSendMessage?.Invoke($"Stopped recording and saved to {mp4path}");

                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
                OnSendMessage?.Invoke("Stopped playback");
                OnPlaybackStatusChanged?.Invoke(false);
            }

            using var recordingConfig = new ArRecordingConfig(subsystem.session);
            mp4path = Path.Combine(Application.persistentDataPath,
                $"arcore-session{DateTime.Now:yyyyMMddHHHmmss}.mp4");
            recordingConfig.SetMp4DatasetFilePath(subsystem.session, mp4path);

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

            if (!File.Exists(mp4path))
            {
                OnSendMessage?.Invoke($"!!!cannot find file path {mp4path}");
                return;
            }

            subsystem.StartPlayback(mp4path);

            OnPlaybackStatusChanged?.Invoke(true);
            
            OnSendMessage?.Invoke("Start playback");
#endif
        }
    }
}