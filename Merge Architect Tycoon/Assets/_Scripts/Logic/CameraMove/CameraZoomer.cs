using DG.Tweening;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class CameraZoomer : MonoBehaviour
{
    public Canvas backGroundCanvas;
    public Camera Camera;
    public Button backgroundButton;

    private bool _zoomedIn = false;
    private Vector2 _yOffset = new(0, -200);
    private Vector2 _baseCameraPosition;
    private string _currentBuildingClicked;

    private void Start()
    {
        _baseCameraPosition = Vector2.zero;
        backgroundButton.onClick.AddListener(MoveCameraBack);
        backGroundCanvas.renderMode = RenderMode.WorldSpace;
    }

    public void ZoomButtonClicked(Transform target, string buildingName)
    {
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
            MoveCameraTo(target);
            _currentBuildingClicked = buildingName;
        }
    }

    private void MoveAndZoomCamera(Transform target)
    {
        ZoomTrue();
        Vector2 newPosition = (Vector2)target.position + _yOffset;
        DOTween
            .To(() => Camera.orthographicSize,
                x =>  Camera.orthographicSize = x, 500, 1f);
        Camera.transform.DOMove(newPosition, 1f);
    }

    public void MoveCameraBack()
    {
        ZoomFalse();
        DOTween.To(() =>  Camera.orthographicSize,
            x =>  Camera.orthographicSize = x, 960, 1f);
        Camera.transform.DOMove(_baseCameraPosition, 1);
    }

    private void ZoomFalse() => _zoomedIn = false;

    private void ZoomTrue() => _zoomedIn = true;

    private void MoveCameraTo(Transform target)
    {
        Vector2 newPosition = (target.position);
        Camera.transform.DOMove(newPosition, 1f);
    }
}