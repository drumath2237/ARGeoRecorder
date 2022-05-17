using System;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

#if UNITY_ANDROID
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

        public void OnStartRecord()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                return;
            }


            var isPlayingOrRecording = subsystem.playbackStatus.Playing() || subsystem.recordingStatus.Recording();
            if (isPlayingOrRecording)
            {
                // todo: recording停止処理を淹れてもいいかも
                return;
            }

            var finishedPlayingAndRecording = subsystem.playbackStatus == ArPlaybackStatus.Finished &&
                                              subsystem.recordingStatus == ArRecordingStatus.None;
            if (!finishedPlayingAndRecording)
            {
                return;
            }

            using var recordingConfig = new ArRecordingConfig(subsystem.session);
            recordingConfig.SetMp4DatasetFilePath(subsystem.session, mp4path);


#endif
        }
    }
}