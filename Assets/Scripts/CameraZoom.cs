using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            Vector3 newPosition = transform.position + transform.forward * scroll * zoomSpeed;
            transform.position = newPosition;
        }
    }
}
