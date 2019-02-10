using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace NPS
{
    public abstract class BaseDictionary : SerializedScriptableObject
    {
        public abstract void Populate();

#if UNITY_EDITOR
        public void SaveDictionary(string _location)
        {
            string currentAssetPath = AssetDatabase.GetAssetPath(this);
            if (currentAssetPath != "")
            {
                Debug.Log("Asset already exists.");
                return;
            }

            string assetName = "";
            if (name == "")
            {
                assetName = $"{GetType().ToString()}.asset";
            }
            else
            {
                assetName = $"{name}.asset";
            }

            string fullAssetPath = $"{_location}\\{assetName}";
            AssetDatabase.CreateAsset(this, fullAssetPath);
            Debug.Log($"Asset created at: {fullAssetPath}");
        }
#endif
    }
}