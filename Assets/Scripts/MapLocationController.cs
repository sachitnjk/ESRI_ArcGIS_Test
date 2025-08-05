using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

	[Header("UI references")]
	[SerializeField] private RectTransform InputPanelTransform;
	[SerializeField] private TMP_InputField latitudeInput;
	[SerializeField] private TMP_InputField longitudeInput;
	[SerializeField] private TMP_InputField altitudeInput;
	[SerializeField] private Button confirmNewInputButton;
	[SerializeField] private Button mobileLocationSetterButton;

	private GameObject inputPanelObject;

	private PlayerInput playerInput;
	private InputAction tabAction;

	private void Start()
	{
		playerInput = InputProvider.GetPlayerInput();
		if(playerInput != null)
		{
			tabAction = playerInput.actions["DebugKey"];
		}

		if(InputPanelTransform != null)
		{
			inputPanelObject = InputPanelTransform.gameObject;
		}

		if(confirmNewInputButton != null)
		{
			confirmNewInputButton.onClick.AddListener(ChangeMapLocation);
		}
		if (mobileLocationSetterButton != null)
		{
			mobileLocationSetterButton.onClick.AddListener(ToggleInputPanelFromMobile);
		}

	}

	private void Update()
	{
		if(tabAction != null && tabAction.triggered && !inputPanelObject.activeInHierarchy)
		{
			inputPanelObject.SetActive(true);
		}
	}

	public void ChangeMapLocation()
	{
		// Try to parse latitude
		if (!double.TryParse(latitudeInput.text, out targetLatitude))
		{
			Debug.LogWarning("Invalid latitude input.");
			return;
		}

		// Try to parse longitude
		if (!double.TryParse(longitudeInput.text, out targetLongitude))
		{
			Debug.LogWarning("Invalid longitude input.");
			return;
		}

		// Try to parse altitude
		if (!double.TryParse(altitudeInput.text, out targetAltitude))
		{
			Debug.LogWarning("Invalid altitude input.");
			return;
		}

		// Creating the new ArcGISPoint
		// NOTE: the ArcGISSpatialReference.WGS84() means that we are specifying ArcGIS to use World GPS cordinates for the passed in targetLongitude and targetLatitude
		ArcGISPoint newPoint = new ArcGISPoint(targetLongitude, targetLatitude, targetAltitude, ArcGISSpatialReference.WGS84());

		// Changign origin of teh map
		arcGISMap.OriginPosition = newPoint;

		// Moving teh camera to hte same place as the map
		arcGISCameraLocation.Position = newPoint;

		if(inputPanelObject.activeInHierarchy)
		{
			inputPanelObject.SetActive(false);
		}
	}

	private void ToggleInputPanelFromMobile()
	{
		if (inputPanelObject.activeInHierarchy)
		{
			inputPanelObject.SetActive(false);
		}
		else
		{
			inputPanelObject.SetActive(true);
		}
	}
}
