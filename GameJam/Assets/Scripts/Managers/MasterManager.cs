using Sirenix.OdinInspector;
using UnityEngine;

public class MasterManager : SerializedMonoBehaviour
{
    private static MasterManager instance;
    public static MasterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MasterManager>();
            }
            return instance;
        }
        private set => instance = value;
    }

    [SerializeField, ReadOnly, PreviewField]
    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }
            return player;
        }
    }

    [SerializeField, ReadOnly, PreviewField]
    private CameraManager cameraManager;
    public CameraManager CameraManager
    {
        get
        {
            if (cameraManager == null)
            {
                cameraManager = FindObjectOfType<CameraManager>();
            }
            return cameraManager;
        }
    }

    private GameObject persistantGameContainer;
    public GameObject PersistantGameContainer
    {
        get
        {
            if (persistantGameContainer == null)
            {
                persistantGameContainer = transform.root.gameObject;
            }
            return persistantGameContainer;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
