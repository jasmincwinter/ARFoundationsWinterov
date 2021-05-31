using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class Scale_Script : MonoBehaviour
{

    [SerializeField]
    private Camera arCamera;

    //private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastmanager;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private float initialDistance;



    RaycastHit hitObject;
    // Start is called before the first frame update

    private Vector3 ScalingY;

    void Awake()
    {
        arRaycastmanager = GetComponent<ARRaycastManager>();


        ScalingY = new Vector3(0, 0.5f, 0);
    }

    private void Update()
    {
        if (Input.touchCount == 2)
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
                Ray ray = arCamera.ScreenPointToRay(touchZero.position);

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.CompareTag("cube"))
                    {

                        onTouchHold = true;

                        initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                        ScalingY = hitObject.transform.localScale;

                    }
                }


            }

            else
            {
                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                Ray ray = arCamera.ScreenPointToRay(touchZero.position);

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.CompareTag("cube"))
                    {

                        onTouchHold = true;

                        var factor = currentDistance / initialDistance;

                        hitObject.transform.localScale = new Vector3(hitObject.transform.localScale.x, ScalingY.y * factor, hitObject.transform.localScale.z);

                    }
                }

            }

        }

    }
}
