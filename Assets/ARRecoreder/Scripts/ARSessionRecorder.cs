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


        private void Awake()
        {
            mp4path = Path.Combine(Application.persistentDataPath, "arcore-session.mp4");
        }

        public void ToggleRecording()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                return;
            }

            if (subsystem.recordingStatus.Recording())
            {
                subsystem.StopRecording();
                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
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
#endif
        }


        public void TogglePlayback()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                return;
            }

            if (subsystem.playbackStatus.Playing())
            {
                subsystem.StopPlayback();
                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                subsystem.StopPlayback();
            }

            if (!File.Exists(mp4path))
            {
                return;
            }

            subsystem.StartPlayback(mp4path);
#endif
        }
    }
}