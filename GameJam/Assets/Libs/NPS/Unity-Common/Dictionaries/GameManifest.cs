using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
# endif

namespace NPS
{
    [CreateAssetMenu(fileName = "GameManifest", menuName = "Game/Manifest")]
    public partial class GameManifest : BaseDictionary
    {
        private static GameManifest _instance;
        public static GameManifest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = MasterManager.Instance.GameManifest;
                    if (_instance == null)
                    {
                        throw new Exception("There are no MasterManagers with a manifest attached! Something is about to break.");
                    }
                }
                return _instance;
            }
        }

        [SerializeField]
        private IconDictionary _iconDictionary;
        public IconDictionary IconDictionary
        {
            get
            {
                if (_iconDictionary == null)
                {
                    _iconDictionary = LoadDictionary<IconDictionary>();
                }
                return _iconDictionary;
            }
        }

        [Button]
        public override void Populate()
        {
            System.Reflection.PropertyInfo[] properties = GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo dictionary in properties)
            {
                if (dictionary.PropertyType.IsSubclassOf(typeof(BaseDictionary)))
                {
                    if (dictionary.GetValue(this) == null)
                    {
                        Debug.Log(dictionary.PropertyType);
                    }
                    (dictionary.GetValue(this) as BaseDictionary).Populate();
                }
                else
                {
                    Debug.Log("hit");
                }
            }
        }

#if UNITY_EDITOR
        [Button]
        public void SaveDictionaries()
        {
            /*string myAssetPath = AssetDatabase.GetAssetPath(this);
            string folderPath = $"{Path.GetDirectoryName(myAssetPath)}\\Dictionaries";

            List<BaseDictionary> availableDictionaries = GetLoadedDictionaries();
            foreach (BaseDictionary dictionary in availableDictionaries)
            {
                dictionary.SaveDictionary(folderPath);
            }*/
        }
#endif

        public BaseDictionary LoadDictionary(Type _dictionaryType)
        {
            string myAssetPath = AssetDatabase.GetAssetPath(this);
            string folderPath = Paths.EditorPaths.ToResourcePath($"{Path.GetDirectoryName(myAssetPath)}\\Dictionaries");
            UnityEngine.Object[] availableDictionaries = Resources.LoadAll(folderPath);
            foreach (UnityEngine.Object dictionary in availableDictionaries)
            {
                if (dictionary.GetType() == _dictionaryType)
                {
                    return dictionary as BaseDictionary;
                }
            }
            return null;
        }

        private T LoadDictionary<T>() where T : BaseDictionary
        {
            T[] resourceList = Resources.FindObjectsOfTypeAll<T>();
            if (resourceList.Length > 1)
            {
                Debug.LogWarning($"Found multiple dictionaries of type {typeof(T).ToString()}.");
                return resourceList[0];
            }
            else if (resourceList.Length == 1)
            {
                return resourceList[0];
            }

            Debug.LogWarning($"Failed to load dictionary of type {typeof(T).ToString()}.");
            return null;
        }
    }
}