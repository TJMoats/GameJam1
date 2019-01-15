using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private SceneDataDictionary scenes;
    public SceneDataDictionary Scenes
    {
        get
        {
            if (scenes == null)
            {
                throw new Exception("Scene Dictionary is missing!");
            }
            return scenes;
        }
    }

    [SerializeField,]
    private SceneData currentScene;
    public SceneData CurrentScene
    {
        get => currentScene;
    }

    public static IEnumerator ChangeScene(string _newSceneName, string _oldSceneName = "", Action _postLoad = null)
    {
        if (IsSceneNameValid(_newSceneName))
        {
            yield return SceneManager.LoadSceneAsync(_newSceneName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_newSceneName));

            _postLoad();

            if (IsSceneNameValid(_oldSceneName))
            {
                yield return new WaitForEndOfFrame();
                SceneManager.UnloadSceneAsync(_oldSceneName);
            }
        }
    }

    private static bool IsSceneNameValid(string _sceneName )
    {
        throw new NotImplementedException();
    }
}
