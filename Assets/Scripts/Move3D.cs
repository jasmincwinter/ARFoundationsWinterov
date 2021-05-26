using UnityEngine;
public class Move3D_Modified : MonoBehaviour
{
    private Camera mainCamera;
    private float CameraZDistance;

    void Start()
    {
        mainCamera = Camera.main;
        CameraZDistance =
            mainCamera.WorldToScreenPoint(transform.position).z; //z axis of the game object for screen view
    }

    void OnMouseDrag()
    {
        Vector3 ScreenPosition =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //z axis added to screen point 
        Vector3 NewWorldPosition =
            mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point
        NewWorldPosition.y = transform.position.y;
        transform.position = NewWorldPosition;
    }
}