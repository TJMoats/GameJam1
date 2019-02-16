using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScrollTransition : Trigger
{
    [SerializeField, ValueDropdown("SceneList")]
    private string targetSceneName;

    protected override void OnCharacterEnter(CharacterController _characterController)
    {
        if (!Triggered && _characterController == MasterManager.Instance.Player)
        {
            Triggered = true;
            WorldManager.ChangeScene(targetSceneName);
        }
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
