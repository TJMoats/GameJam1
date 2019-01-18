using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Trigger
{
    [SerializeField, ValueDropdown("SceneList")]
    private string targetSceneName;

    [SerializeField]
    private string targetTransitionId;

    [SerializeField]
    private string transitionId = System.Guid.NewGuid().ToString();
    public string TransitionId
    {
        get
        {
            if (string.IsNullOrEmpty(TransitionId))
            {
                transitionId = System.Guid.NewGuid().ToString();
            }
            return transitionId;
        }
    }

    [SerializeField]
    private Transform spawnPoint;
    public Transform SpawnPoint
    {
        get
        {
            if (spawnPoint == null)
            {
                Transform sp = transform.Find("SpawnPoint");
                if (sp == null)
                {
                    GameObject spgo = new GameObject("SpawnPoint");
                    spgo.transform.SetParent(transform);
                    spgo.transform.localPosition = Vector3.zero;
                    sp = spgo.transform;
                }
                spawnPoint = sp;
            }
            return spawnPoint;
        }
        private set => spawnPoint = value;
    }

    protected override void OnCharacterEnter(CharacterController _characterController)
    {
        if (!Triggered && _characterController == MasterManager.Instance.Player)
        {
            Triggered = true;
            WorldManager.ChangeScene(targetSceneName, targetTransitionId);
        }
    }

    private void OnValidate()
    {
        SpawnPoint = SpawnPoint;
    }

    private void OnDrawGizmos()
    {
        if (SpawnPoint != null)
        {
            Gizmos.color = new Color(0, .2f, .8f, .5f);
            Gizmos.DrawSphere(SpawnPoint.position, .5f);
        }
    }

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
}
