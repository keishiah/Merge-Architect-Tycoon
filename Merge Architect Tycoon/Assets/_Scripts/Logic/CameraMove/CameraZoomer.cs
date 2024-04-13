using DG.Tweening;
using UnityEngine;
using Button = UnityEngine.UI.Button;


public class CameraZoomer : MonoBehaviour
{
    public Camera mainCamera;
    public Button BackgroundButton;

    private bool _zoomedIn = false;
    private bool _inProgress;
    private Vector2 _baseCameraPosition;


    private void Start()
    {
        _baseCameraPosition = mainCamera.transform.position;
        BackgroundButton.onClick.AddListener(MoveCameraBack);
    }

    public void ZoomButtonClicked(Transform target)
    {
        if (_inProgress)
            return;
        if (_zoomedIn)
        {
            MoveCamera(target);
        }

        if (!_zoomedIn)
        {
            MoveAndZoomCamera(target);
        }
    }

    private async void MoveCameraBack()
    {
        print("back");
        if (_inProgress || !_zoomedIn)
            return;
        _inProgress = true;
        mainCamera.transform.DOMove(_baseCameraPosition, 1);
        await mainCamera.DOOrthoSize(mainCamera.orthographicSize * 2, 1).AsyncWaitForCompletion();
        _inProgress = false;
        _zoomedIn = false;
    }

    private async void MoveCamera(Transform target)
    {
        _inProgress = true;
        Vector2 newPosition = (target.position);
        await mainCamera.transform.DOMove(newPosition, 1f).AsyncWaitForCompletion();
        _inProgress = false;
    }
    private async void MoveAndZoomCamera(Transform target)
    {
        _inProgress = true;
        Vector2 newPosition = (target.position);
        mainCamera.transform.DOMove(newPosition, 1f);
        await mainCamera.DOOrthoSize(mainCamera.orthographicSize / 2f, 1f).AsyncWaitForCompletion();
        _inProgress = false;
        _zoomedIn = true;
    }
}