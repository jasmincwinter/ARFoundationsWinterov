using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; 

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject; 

    private Pose placementPose;
    private ARRaycastManager arRaycastManager;
    private bool placementPoseIsValid = false; 

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if(spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPLaceObject(); 
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
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
        arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void ARPLaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, placementPose.position, placementPose.rotation); 
    }




}
