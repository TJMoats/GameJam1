using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
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

    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        Debug.Log("OnSceneLoaded: " + _scene.name);
        Debug.Log(_mode);
    }
}
