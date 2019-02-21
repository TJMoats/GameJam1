using NPS;
using Sirenix.OdinInspector;
using UnityEngine;

public class MasterManager : NPS.MasterManager
{
    private static MasterManager instance;
    public static new MasterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MasterManager>();
                if (instance == null)
                {
                    Debug.Log("The master manager doesn't exist. Creating it now.");
                    instance = LoadMasterManager();
                }
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

    private static MasterManager LoadMasterManager()
    {
        GameObject masterManager = Instantiate(Resources.Load<GameObject>("Prefabs/Managers/MasterManager"));
        masterManager.name = "Master Manager";
        PlayerController.Instance.gameObject.transform.position = SpawnPoint.GetSpawnPosition("");
        return masterManager.GetComponent<MasterManager>();
    }

    private void Awake()
    {
        Instance = this;
    }
}
