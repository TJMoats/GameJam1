using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NPS
{
    public abstract class MasterManager : SerializedMonoBehaviour
    {
        private static MasterManager _instance;
        public static MasterManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    MasterManager[] availableMasterManagers = FindObjectsOfType<MasterManager>();
                    if (availableMasterManagers.Length == 0)
                    {
                        throw new Exception("There are no master managers in this scene! Something is about to break.");
                    }
                    else if (availableMasterManagers.Length > 1)
                    {
                        throw new Exception("There are too many master managers in this scene! Something is about to break.");
                    }
                    _instance = availableMasterManagers[0];
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        [SerializeField]
        private GameManifest _gameManifest;
        public GameManifest GameManifest
        {
            get
            {
                if (_gameManifest == null)
                {
                    GameManifest[] availableManifests = Resources.LoadAll<GameManifest>(Paths.ResourcePaths.manifestPath);
                    if (availableManifests.Length > 0)
                    {
                        _gameManifest = availableManifests.Last();
                    }
                }
                return _gameManifest;
            }
            private set
            {
                _gameManifest = value;
            }
        }

        [SerializeField]
        private Camera _mainCamera;
        public static Camera MainCamera
        {
            get
            {
                if (Instance._mainCamera == null)
                {
                    Instance._mainCamera = Camera.main;
                    if (Instance._mainCamera == null)
                    {
                        Camera[] cameras = FindObjectsOfType<Camera>();
                        if (cameras.Length != 1)
                        {
                            throw new System.Exception("No Cameras in Scene");
                        }
                        Instance._mainCamera = cameras[0];
                    }
                }
                return Instance._mainCamera;
            }
        }

        private void OnEnable()
        {
            //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        private void OnDisable()
        {
            //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        private void OnLevelFinishedLoading(Scene _scene, LoadSceneMode _mode)
        {
            Instance = null;
        }
        
        protected virtual void OnValidate()
        {
            if (gameObject.activeInHierarchy)
            {
                GameManifest = GameManifest;
            }
        }
    }
}