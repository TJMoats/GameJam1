using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class CameraManager : SerializedMonoBehaviour
{
    [SerializeField, ReadOnly, PreviewField]
    private Camera mainCamera;
    public Camera MainCamera
    {
        get
        {
            if (mainCamera == null)
            {
                mainCamera = GetComponentInChildren<Camera>();
            }
            return mainCamera;
        }
    }

    private CinemachineVirtualCamera cinemachine;
    public CinemachineVirtualCamera Cinemachine
    {
        get
        {
            if (cinemachine == null)
            {
                cinemachine = GetComponentInChildren<CinemachineVirtualCamera>();
            }
            return cinemachine;
        }
    }

    [SerializeField]
    private CinemachineConfiner cinemachineConfiner;
    public CinemachineConfiner CinemachineConfiner
    {
        get
        {
            if (cinemachineConfiner == null)
            {
                cinemachineConfiner = GetComponentInChildren<CinemachineConfiner>();
                if (cinemachineConfiner == null)
                {
                    cinemachineConfiner = Cinemachine?.gameObject.GetOrAddComponent<CinemachineConfiner>();
                }
            }
            return cinemachineConfiner;
        }
    }

    private void Awake()
    {
        ChangeBounds();
        NPS.SceneHelper.primarySceneChanged += ChangeBounds;
        CinemachineConfiner.m_BoundingShape2D = CameraBounds;
        StartCoroutine(DelayDampening());
    }

    private IEnumerator DelayDampening()
    {
        yield return new WaitForSeconds(.1f);
        CinemachineConfiner.m_Damping = 1;
    }

    private void ChangeBounds(NPS.SceneLoader _sceneLoader = null)
    {
        if (_sceneLoader == null)
        {
            CloneBounds(FindObjectOfType<SceneController>().SceneBounds);
        }
        else
        {
            SceneController sceneController = _sceneLoader.sceneController as SceneController;
            CloneBounds(sceneController.SceneBounds);
        }
    }

    private PolygonCollider2D cameraBounds;
    public PolygonCollider2D CameraBounds
    {
        get
        {
            if (cameraBounds == null)
            {
                cameraBounds = gameObject.GetOrAddComponent<PolygonCollider2D>();
                cameraBounds.isTrigger = true;
            }
            return cameraBounds;
        }
    }

    private void CloneBounds(PolygonCollider2D _sceneBounds)
    {
        Vector2[] newBounds = new Vector2[_sceneBounds.points.Length];
        for (int i = 0; i < newBounds.Length; i++)
        {
            newBounds[i] = _sceneBounds.points[i] + ((Vector2)_sceneBounds.gameObject.transform.position);
            newBounds[i].x += (_sceneBounds.points[i].x > 0 ? 1 : -1);
            newBounds[i].y += (_sceneBounds.points[i].y > 0 ? 1 : -1);
        }
        CameraBounds.points = newBounds;
        CinemachineConfiner.InvalidatePathCache();
    }
}
