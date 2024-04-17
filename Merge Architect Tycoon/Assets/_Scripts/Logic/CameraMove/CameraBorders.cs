using UnityEngine;

public class CameraBorders : MonoBehaviour
{
    public Canvas canvas;
    private Vector3 desiredPosition;
    private Camera cam;
    private RectTransform canvasRect;
    private Vector2 canvasSize;
    private float cameraHalfHeight;
    private float cameraHalfWidth;


    void Start()
    {
        cam = GetComponent<Camera>();
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasSize = canvasRect.sizeDelta * canvasRect.lossyScale;
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, canvasRect.position.x - canvasSize.x / 2 + cameraHalfWidth,
            canvasRect.position.x + canvasSize.x / 2 - cameraHalfWidth);
        newPos.y = Mathf.Clamp(newPos.y, canvasRect.position.y - canvasSize.y / 2 + cameraHalfHeight-100,
            canvasRect.position.y + canvasSize.y / 2 - cameraHalfHeight);
        transform.position = newPos;
    }
}