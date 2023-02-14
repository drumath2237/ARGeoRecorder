using System.Text;
using Google.XR.ARCoreExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace ARRecorder
{
    public class GeospatialStatusView : MonoBehaviour
    {
        [SerializeField] private AREarthManager earthManager;

        [SerializeField] private TextMeshProUGUI logText;


        private EarthState _earthState;
        private TrackingState _trackingState;
        private FeatureSupported _featureSupported;
        private GeospatialPose _geospatialPose;

        private void Update()
        {
            _earthState = earthManager.EarthState;
            _trackingState = earthManager.EarthTrackingState;

            _featureSupported = earthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);

            _geospatialPose = earthManager.CameraGeospatialPose;

            if (logText != null)
            {
                logText.text = CreateLogString();
            }
        }

        private string CreateLogString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(_earthState.ToString());
            stringBuilder.AppendLine(_trackingState.ToString());
            stringBuilder.AppendLine(_featureSupported.ToString());

            stringBuilder.AppendLine(
                $"pos: {_geospatialPose.Latitude},{_geospatialPose.Longitude},{_geospatialPose.Altitude}");
            stringBuilder.AppendLine($"acc: {_geospatialPose.HorizontalAccuracy},{_geospatialPose.VerticalAccuracy}");

            return stringBuilder.ToString();
        }
    }
}