using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectToCreate;

    public GameObject PlacedPrefab
    {
        get
        {
            return PlacedPrefab;
        }

        set
        {
            PlacedPrefab = value;
        }
    }

    private ARRaycastManager arRaycastManager; 

    private void Awake()
    {

        arRaycastManager = GetComponent<ARRaycastManager>(); 
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true; 
        }

        touchPosition = default; 
        return false; 
    }


    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosotion))
            return;

        if (arRaycastManager.Raycast(touchPosotion, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation); 
        }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); 
}
