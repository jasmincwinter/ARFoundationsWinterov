using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class SelectAndMove : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;

    [SerializeField]
    private Camera arCamera;

    private GameObject placedObject;
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastmanager;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    RaycastHit hitObject;
    // Start is called before the first frame update
    void Awake()
    {
        arRaycastmanager = GetComponent<ARRaycastManager>();

        cube1.SetActive(true);
        cube2.SetActive(true); 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;


            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.CompareTag("cube"))
                    {

                        onTouchHold = true;
                    }
                }

            }

            if (touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;
            }
        }

        if (arRaycastmanager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (onTouchHold == true)
            {

                hitObject.transform.position = hitPose.position;
                hitObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}
