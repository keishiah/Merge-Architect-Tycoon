using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class CameraZoomer : MonoBehaviour
{
    // public Camera mainCamera;
    public CinemachineVirtualCamera CinemachineVirtualCamera;
    public Button BackgroundButton;

    private bool _zoomedIn = false;
    private bool _inProgress;
    private Vector2 _baseCameraPosition;
    private string _currentBuildingClicked;

    private void Start()
    {
        _baseCameraPosition = CinemachineVirtualCamera.transform.position;
        BackgroundButton.onClick.AddListener(MoveCameraBack);
    }

    public void ZoomButtonClicked(Transform target, string buildingName)
    {
        if (_inProgress)
            return;

        if (!_zoomedIn)
        {
            MoveAndZoomCamera(target);
            _currentBuildingClicked = buildingName;
        }
        else if (_currentBuildingClicked == buildingName)
        {
            MoveCameraBack();
        }
        else
        {
            MoveCamera(target);
            _currentBuildingClicked = buildingName;
        }
    }

    public async void MoveCameraBack()
    {
        if (_inProgress || !_zoomedIn)
            return;
        _inProgress = true;
        CinemachineVirtualCamera.transform.DOMove(_baseCameraPosition, 1);
        DOTween.To(() => CinemachineVirtualCamera.m_Lens.OrthographicSize,
                x => CinemachineVirtualCamera.m_Lens.OrthographicSize = x, 960, 1)
            .OnComplete(ZoomFalse);
        // await mainCamera.DOOrthoSize(960, 1).AsyncWaitForCompletion();
        // _inProgress = false;
        // _zoomedIn = false;
    }

    private void ZoomFalse()
    {
        _inProgress = false;
        _zoomedIn = false;
    }

    private void ZoomTrue()
    {
        _inProgress = false;
        _zoomedIn = true;
    }
    private async void MoveCamera(Transform target)
    {
        _inProgress = true;
        Vector2 newPosition = (target.position);
        await CinemachineVirtualCamera.transform.DOMove(newPosition, 1f).AsyncWaitForCompletion();
        _inProgress = false;
    }

    private async void MoveAndZoomCamera(Transform target)
    {
        _inProgress = true;
        Vector2 newPosition = (target.position);
        CinemachineVirtualCamera.transform.DOMove(newPosition, 1f);
         DOTween
            .To(() => CinemachineVirtualCamera.m_Lens.OrthographicSize,
                x => CinemachineVirtualCamera.m_Lens.OrthographicSize = x, 500, 1).OnComplete(ZoomTrue);
         // _inProgress = false;
         // _zoomedIn = true;
    }
}