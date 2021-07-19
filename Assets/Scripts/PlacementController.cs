using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{

    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;

    private GameObject spawnObject;

    private Pose PlacementPose;
    private ARRaycastManager arraycastManager;
    private bool placementPoseIsValid = false;

    private ARPlaneManager arPlaneManangerScript; 

    //private float initialDistance;
    //private Vector3 initialScale;



    // Start is called before the first frame update
    void Start()
    {
        arraycastManager = FindObjectOfType<ARRaycastManager>();

        arPlaneManangerScript = GetComponent<ARPlaneManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (spawnObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();

        }

        //if (Input.touchCount == 2)
        //{
        //    var touchZero = Input.GetTouch(0);
        //    var touchOne = Input.GetTouch(1);

        //    if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
        //       touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
        //    {
        //        return;
        //    }

        //    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
        //    {
        //        initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
        //        initialScale = spawnObject.transform.localScale;
        //        Debug.Log("initial distance: " + initialDistance + "gameObject name: " + arObjectToSpawn.name);
        //    }

        //    else
        //    {
        //        var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

        //        if (Mathf.Approximately(initialDistance, 0))
        //        {
        //            return;
        //        }

        //        var factor = currentDistance / initialDistance;
        //        spawnObject.transform.localScale = initialScale * factor;
        //    }

        //}

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void UpdatePlacementIndicator()
    {
        if (spawnObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }

        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arraycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    void ARPlaceObject()
    {
        spawnObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);

        arPlaneManangerScript.enabled = false;
    }
}


//{
//    [SerializeField]
//    private GameObject gameObjectToCreate;

//    public GameObject PlacedPrefab
//    {
//        get
//        {
//            return PlacedPrefab;
//        }

//        set
//        {
//            PlacedPrefab = value;
//        }
//    }

//    private ARRaycastManager arRaycastManager; 

//    private void Awake()
//    {

//        arRaycastManager = GetComponent<ARRaycastManager>(); 
//    }

//    bool TryGetTouchPosition(out Vector2 touchPosition)
//    {
//        if(Input.touchCount > 0)
//        {
//            touchPosition = Input.GetTouch(0).position;
//            return true; 
//        }

//        touchPosition = default; 
//        return false; 
//    }


//    void Update()
//    {
//        if (!TryGetTouchPosition(out Vector2 touchPosotion))
//            return;

//        if (arRaycastManager.Raycast(touchPosotion, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
//        {
//            var hitPose = hits[0].pose;

//            Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation); 
//        }
//    }

//    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); 
//}
