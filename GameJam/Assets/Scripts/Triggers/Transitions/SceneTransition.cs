using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class SceneTransition : Trigger
{
    [SerializeField, ValueDropdown("SceneList")]
    private string targetSceneName;
    public string TargetSceneName
    {
        get => targetSceneName;
    }

    [SerializeField]
    public abstract TransitionType TransitionType
    {
        get;
    }
    
#if UNITY_EDITOR
    private IList<ValueDropdownItem<string>> SceneList()
    {
        ValueDropdownList<string> valueDropdownItems = new ValueDropdownList<string>();
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            valueDropdownItems.Add(sceneName, sceneName);
        }
        return valueDropdownItems;
    }
#endif
}

public enum TransitionType
{
    scroll,
    fade
}
