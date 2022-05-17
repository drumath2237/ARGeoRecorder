using System;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

namespace ARRecorder
{
    public class ARSessionRecorder : MonoBehaviour
    {
        [SerializeField] private ARSession arSession;

        public void OnStartRecord()
        {
#if UNITY_ANDROID
            if (arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                return;
            }


#endif
        }
    }
}