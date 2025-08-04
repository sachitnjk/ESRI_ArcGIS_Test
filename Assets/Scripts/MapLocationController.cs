using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using UnityEngine;

public class MapLocationController : MonoBehaviour
{
	[field: SerializeField] public ArcGISMapComponent arcGISMap { get; private set; }
	[field: SerializeField] public ArcGISLocationComponent arcGISCameraLocation { get; private set; }

	[Header("Custom Values")]
	[Tooltip("Debug latitude assigned by default")]
	[SerializeField] private double targetLatitude = 17.339001665952576;
	[Tooltip("Debug longitude assigned by default")]
	[SerializeField] private double targetLongitude = 78.37873951206078;
	[SerializeField] private double targetAltitude = 100;

	public void ChangeMapLocation()
	{
		// Create the new ArcGISPoint
		// NOTE: the ArcGISSpatialReference.WGS84() means that we are specifying ArcGIS to use World GPS cordinates for the passed in targetLongitude and targetLatitude
		ArcGISPoint newPoint = new ArcGISPoint(targetLongitude, targetLatitude, targetAltitude, ArcGISSpatialReference.WGS84());

		// Step 1: Change the map origin
		arcGISMap.OriginPosition = newPoint;

		// Step 2: Move the camera to the same point
		arcGISCameraLocation.Position = newPoint;
	}
}
