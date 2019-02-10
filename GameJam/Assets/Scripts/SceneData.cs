using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scene", menuName = "Data/Scene")]
public class SceneData : SerializedScriptableObject
{
    [SerializeField]
    private string sceneName;
    public string SceneName
    {
        get => sceneName;
        set => sceneName = value;
    }

    [SerializeField]
    private string sceneTitle;

    private List<SceneTransition> sceneTransitions;
    public List<SceneTransition> SceneTransitions
    {
        get
        {
            if (sceneTransitions == null)
            {
                sceneTransitions = new List<SceneTransition>();
            }
            return sceneTransitions;
        }
    }
}