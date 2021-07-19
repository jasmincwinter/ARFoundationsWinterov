using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class SelectAndMove : MonoBehaviour
{
    //public GameObject cube1;
    //public GameObject cube2;

    [SerializeField]
    private Camera arCamera;

    private GameObject placedObject;
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastmanager;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    RaycastHit hitObject;


    private float initialDistance;
    private Vector3 initialScale;

    //private GameObject spawnObject;
    public GameObject arObjectToSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        arRaycastmanager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount == 1) // Does this have to be > 0? 
        //{
        //    Touch touch = Input.GetTouch(0);
        //    touchPosition = touch.position;


        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Ray ray = arCamera.ScreenPointToRay(touch.position);

        //        if (Physics.Raycast(ray, out hitObject))
        //        {
        //            if (hitObject.transform.CompareTag("cube"))
        //            {
        //                onTouchHold = true;
        //            }
        //        }

        //    }

        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        onTouchHold = false;
        //    }
        //}

        //if (arRaycastmanager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        //{
        //    Pose hitPose = hits[0].pose;

        //    if (onTouchHold == true)
        //    {

        //        hitObject.transform.position = hitPose.position;
        //        hitObject.transform.rotation = hitPose.rotation;
        //    }
        //}

        // Copied from PlacementController script. 

        if (Input.touchCount == 2 && hitObject.transform.CompareTag("cube"))
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
               touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }

            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = hitObject.transform.localScale; // Changed from spawnObject. 
                Debug.Log("initial distance: " + initialDistance + "gameObject name: " + arObjectToSpawn.name);
            }

            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                var factor = currentDistance / initialDistance;
                hitObject.transform.localScale = initialScale * factor;
            }

        }
    }
}
